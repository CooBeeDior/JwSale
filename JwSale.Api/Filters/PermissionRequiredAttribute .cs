﻿using JwSale.Api.Util;
using JwSale.Model.Dto;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;

namespace JwSale.Api.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PermissionRequiredAttribute : Attribute, IAuthorizationFilter
    {

        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private JwSaleDbContext jwSaleDbContext;


        public PermissionRequiredAttribute(JwSaleDbContext jwSaleDbContext, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions)
        {
            this.jwSaleDbContext = jwSaleDbContext;
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var isDefined = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(NoPermissionRequiredAttribute)));
                if (isDefined)
                {
                    return;
                }
                isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(NoPermissionRequiredAttribute)));
                if (isDefined)
                {
                    return;
                }
            }
            string token = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                ResponseBase response = new ResponseBase();
                response.Success = false;
                response.Code = HttpStatusCode.Unauthorized;
                response.Message = "请传入令牌";
                context.Result = new JsonResult(response);
            }
            else
            {

                var userToken = UserHelper.AnalysisToken(token, jwSaleOptions.TokenKey);
                if (userToken == null)
                {
                    ResponseBase response = new ResponseBase();
                    response.Success = false;
                    response.Code = HttpStatusCode.Unauthorized;
                    response.Message = "未知的令牌";
                    context.Result = new JsonResult(response);
                }
                else
                {
                    var userTokenCache = cache.GetString(CacheKeyHelper.GetUserTokenKey(userToken.UserName));
                    if (string.IsNullOrEmpty(userTokenCache))
                    {
                        ResponseBase response = new ResponseBase();
                        response.Success = false;
                        response.Code = HttpStatusCode.Unauthorized;
                        response.Message = "令牌失效";
                        context.Result = new JsonResult(response);
                    }
                    else
                    {
                        var userInfo = jwSaleDbContext.UserInfos.Where(o => o.Id == userToken.UserId).FirstOrDefault();
                        if (userInfo == null)
                        {
                            ResponseBase response = new ResponseBase();
                            response.Success = false;
                            response.Code = HttpStatusCode.Unauthorized;
                            response.Message = "未知的用户";
                            context.Result = new JsonResult(response);
                        }
                        else
                        {
                            context.HttpContext.Items[CacheKeyHelper.GetHttpContextUserKey()] = userInfo;
                        }
                    }

                }
            }


        }

        //public   void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (!filterContext.ModelState.IsValid)
        //    {
        //        var errors = filterContext.ModelState.Values.Select(x => x.Errors);
        //        ResponseBase response = new ResponseBase();
        //        response.Success = false;
        //        response.Code = HttpStatusCode.BadRequest;
        //        response.Message = errors.ToJson();
        //        filterContext.Result = new JsonResult(response);
        //    }
        //    else
        //    {
        //        var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
        //        if (controllerActionDescriptor != null)
        //        {
        //            var isDefined = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(NoPermissionRequiredAttribute)));
        //            if (isDefined)
        //            {
        //                return;
        //            }
        //            isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(NoPermissionRequiredAttribute)));
        //            if (isDefined)
        //            {
        //                return;
        //            }
        //        }
        //        string token = filterContext.HttpContext.Request.Headers["Authorization"].ToString();
        //        if (string.IsNullOrEmpty(token))
        //        {
        //            ResponseBase response = new ResponseBase();
        //            response.Success = false;
        //            response.Code = HttpStatusCode.Unauthorized;
        //            response.Message = "请传入令牌";
        //            filterContext.Result = new JsonResult(response);
        //        }
        //        else
        //        {

        //            var userToken = UserHelper.AnalysisToken(token, jwSaleOptions.TokenKey);
        //            if (userToken == null)
        //            {
        //                ResponseBase response = new ResponseBase();
        //                response.Success = false;
        //                response.Code = HttpStatusCode.Unauthorized;
        //                response.Message = "未知的令牌";
        //                filterContext.Result = new JsonResult(response);
        //            }
        //            else
        //            {
        //                var userTokenCache = cache.GetString(CacheKeyHelper.GetUserTokenKey(userToken.UserName));
        //                if (string.IsNullOrEmpty(userTokenCache))
        //                {
        //                    ResponseBase response = new ResponseBase();
        //                    response.Success = false;
        //                    response.Code = HttpStatusCode.Unauthorized;
        //                    response.Message = "令牌失效";
        //                    filterContext.Result = new JsonResult(response);
        //                }
        //                else
        //                {
        //                    var userInfo = jwSaleDbContext.UserInfos.Where(o => o.Id == userToken.UserId).FirstOrDefault();
        //                    if (userInfo == null)
        //                    {
        //                        ResponseBase response = new ResponseBase();
        //                        response.Success = false;
        //                        response.Code = HttpStatusCode.Unauthorized;
        //                        response.Message = "未知的用户";
        //                        filterContext.Result = new JsonResult(response);
        //                    }
        //                    else
        //                    {
        //                        filterContext.HttpContext.Items[CacheKeyHelper.GetHttpContextUserKey()] = userInfo;
        //                    }
        //                }

        //            }
        //        }

        //    }


        //    base.OnActionExecuting(filterContext);
        //}



        //ResponseBase response = new ResponseBase();
        //response.Success = false;
        //response.Code = HttpStatusCode.Unauthorized;
        //response.Message = "权限不足";
        //filterContext.Result = new JsonResult(response);
    }
}

