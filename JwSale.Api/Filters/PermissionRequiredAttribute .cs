using JwSale.Api.Util;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
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
            string token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","",StringComparison.OrdinalIgnoreCase);
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
                //else if (userToken.Ip != context.HttpContext.Connection.RemoteIpAddress.ToString())
                //{
                //    ResponseBase response = new ResponseBase();
                //    response.Success = false;
                //    response.Code = HttpStatusCode.Unauthorized;
                //    response.Message = "请求IP异常";
                //    context.Result = new JsonResult(response);
                //}
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

                        var userCache = userTokenCache.ToObj<UserCache>();
                        if (userCache == null || userCache.UserInfo == null)
                        {
                            ResponseBase response = new ResponseBase();
                            response.Success = false;
                            response.Code = HttpStatusCode.Unauthorized;
                            response.Message = "令牌失效";
                            context.Result = new JsonResult(response);
                        }
                        else
                        {

                            var userInfo = jwSaleDbContext.UserInfos.Where(o => o.Id == userCache.UserInfo.Id).FirstOrDefault();
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
                                if (userInfo.Status != 0)
                                {
                                    ResponseBase response = new ResponseBase();
                                    response.Success = false;
                                    response.Code = HttpStatusCode.Unauthorized;
                                    response.Message = "用户被禁用，请联系管理员开启";
                                    context.Result = new JsonResult(response);
                                }
                                else if (userInfo.ExpiredTime == null || userInfo.ExpiredTime < DateTime.Now)
                                {
                                    ResponseBase response = new ResponseBase();
                                    response.Success = false;
                                    response.Code = HttpStatusCode.Unauthorized;
                                    response.Message = "用户已过期";
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
            }


        }


    }
}

