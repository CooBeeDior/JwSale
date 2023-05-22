using JsSaleService;
using JwSale.Api.Attributes;
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
    public class WechatAuthFilterAttribute : Attribute, IAuthorizationFilter
    {

        private readonly IDistributedCache _cache;
        private readonly JwSaleOptions _jwSaleOptions;
        private readonly IUserService _userService; 

        public WechatAuthFilterAttribute(IDistributedCache cache,
            IOptions<JwSaleOptions> jwSaleOptions, IUserService userService)
        {

            _cache = cache;
            _jwSaleOptions = jwSaleOptions.Value;
            _userService = userService;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            bool isAuth = false;
            if (controllerActionDescriptor != null)
            {
                var wechatNoAuthRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<WechatAuthRequiredAttribute>(true);
                if (wechatNoAuthRequiredAttribute != null)
                {
                    isAuth = true;
                    goto Tag;
                }
                var noWechatNoAuthRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                if (noWechatNoAuthRequiredAttribute != null)
                {
                    isAuth = false;
                    goto Tag;
                }

                var wechatNoAuControllerRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<WechatAuthRequiredAttribute>(true);
                if (wechatNoAuControllerRequiredAttribute != null)
                {
                    isAuth = true;
                    goto Tag;
                }

                var noWechatNoAuControllerRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<WechatNoAuthRequiredAttribute>(true);
                if (noWechatNoAuControllerRequiredAttribute != null)
                {
                    isAuth = false;
                    goto Tag;
                }


            }

        Tag:
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
                var wechatUserStr = _cache.GetString(CacheKeyHelper.GetWxUserKey(openId));
                WechatUserCache wechatUserCache = null;
                if (!string.IsNullOrWhiteSpace(wechatUserStr))
                {
                    wechatUserCache = wechatUserStr.ToObj<WechatUserCache>();

                }
                else
                {
                    var wechatUser = _userService.GetWechatUser(openId);
                    if (wechatUser != null)
                    {
                        wechatUserCache = wechatUser.DeepCopyByReflection<WechatUserCache>();
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
                    _cache.SetString(CacheKeyHelper.GetWxUserKey(openId), wechatUserCache.ToJson());
                    context.HttpContext.Items[CacheKeyHelper.WECHATUSER] = wechatUserCache;
                }


            }



        }







    }
}
