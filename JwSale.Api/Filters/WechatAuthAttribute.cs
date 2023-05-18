using JsSaleService;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Reflection;
namespace JwSale.Api.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WechatAuthAttribute : Attribute, IAuthorizationFilter
    {

        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private JwSaleDbContext jwSaleDbContext;
        private readonly IHospitalService _hospitalService;


        public WechatAuthAttribute(JwSaleDbContext jwSaleDbContext, IDistributedCache cache,
            IOptions<JwSaleOptions> jwSaleOptions, IHospitalService hospitalService)
        {
            this.jwSaleDbContext = jwSaleDbContext;
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            _hospitalService = hospitalService;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            bool isAuth = false;
            if (controllerActionDescriptor != null)
            {
                var permissionRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                if (permissionRequiredAttribute != null)
                {
                    isAuth = true;
                }
                var authRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<AuthRequiredAttribute>(true);
                if (authRequiredAttribute != null)
                {
                    isAuth = true;
                }

                if (!isAuth)
                {
                    var noAuthRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                    if (noAuthRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                    noAuthRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                    if (noAuthRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                    var noPermissionRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                    if (noPermissionRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                    noPermissionRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                    if (noPermissionRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                }
            }
            if (isAuth == false)
            {
                return;
            }
            var openId = context.HttpContext.WxOpenId();
            if (string.IsNullOrWhiteSpace(openId))
            {
                ResponseBase response = new ResponseBase();
                response.Success = false;
                response.Code = HttpStatusCode.Unauthorized;
                response.Message = "未授权";
                context.Result = new JsonResult(response);
            }
            else
            {
                var wechatUserStr = cache.GetString(CacheKeyHelper.GetWxUserKey(openId));
                WechatUserCache wechatUserCache = null;
                if (!string.IsNullOrWhiteSpace(wechatUserStr))
                {
                    wechatUserCache = wechatUserStr.ToObj<WechatUserCache>();

                }
                else
                {
                    var wechatUser = _hospitalService.GetWechatUser(openId, context.HttpContext.HospitalId()).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (wechatUser != null)
                    {
                        var cc =111wechatUserCache.DeepCopyByReflection<WechatUserCache>();
                    }

                }



                if (wechatUserCache == null)
                {
                    ResponseBase response = new ResponseBase();
                    response.Success = false;
                    response.Code = HttpStatusCode.Unauthorized;
                    response.Message = "授权用户信息不存在";
                    context.Result = new JsonResult(response);

                }
                else
                {
                    cache.SetString(CacheKeyHelper.GetWxUserKey(openId), wechatUserCache.ToJson());
                    context.HttpContext.Items[CacheKeyHelper.WECHATUSER] = wechatUserCache;
                }


            }



        }







    }
}
