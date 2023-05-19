using JwSale.Api.Attributes;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Packs.Attributes;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace JwSale.Api.Filters
{

    /// <summary>
    /// 模块授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PermissionRequiredFilterAttribute : Attribute, IAuthorizationFilter
    {
        private IDistributedCache cache;
        private JwSaleDbContext jwSaleDbContext;
        public PermissionRequiredFilterAttribute(JwSaleDbContext jwSaleDbContext, IDistributedCache cache)
        {
            this.jwSaleDbContext = jwSaleDbContext;
            this.cache = cache;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            bool isAuth = false;
            if (controllerActionDescriptor != null)
            {
                var permissionRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<PermissionRequiredAttribute>(true);
                if (permissionRequiredAttribute != null)
                {
                    isAuth = true;
                    goto Tag;
                }
                var noPermissionRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<NoPermissionRequiredAttribute>(true);
                if (noPermissionRequiredAttribute != null)
                {
                    isAuth = false;
                    goto Tag;
                }
                var noAuthRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<NoAuthRequiredAttribute>(true);
                if (noAuthRequiredAttribute != null)
                {
                    isAuth = false;
                    goto Tag;
                }
                var noAuthControllerRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<NoAuthRequiredAttribute>(true);
                if (noAuthControllerRequiredAttribute != null)
                {
                    isAuth = false;
                    goto Tag;
                }

                var permissionControllerRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<PermissionRequiredAttribute>(true);
                if (permissionControllerRequiredAttribute != null)
                {
                    isAuth = true;
                    goto Tag;
                }

                var noPermissionControllerRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<NoPermissionRequiredAttribute>(true);
                if (noPermissionControllerRequiredAttribute != null)
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
            var userinfo = context.HttpContext.Items[CacheKeyHelper.CURRENTUSER] as UserInfo;
            if (userinfo == null)
            {
                ResponseBase response = new ResponseBase();
                response.Success = false;
                response.Code = HttpStatusCode.Unauthorized;
                response.Message = "登录用户未找到";
                context.Result = new JsonResult(response);
            }
            else
            {
                var controllerMoudleInfo = controllerActionDescriptor?.ControllerTypeInfo?.GetCustomAttribute<MoudleInfoAttribute>();
                var actionMoudleInfo = controllerActionDescriptor?.MethodInfo?.GetCustomAttribute<MoudleInfoAttribute>();
                string loginDevice = context.HttpContext.LoginDevice();
                var userCache = cache.GetString(CacheKeyHelper.GetLoginUserKey(userinfo.UserName, loginDevice))?.ToObj<UserCache>();

                string code = string.IsNullOrEmpty(controllerMoudleInfo.Code) ? controllerMoudleInfo.Name.ToPinYin() : controllerMoudleInfo.Code;
           
                code = string.IsNullOrEmpty(actionMoudleInfo.Code) ? actionMoudleInfo.Name.ToPinYin() : actionMoudleInfo.Code;
                var permission = userCache?.Permissions?.FirstOrDefault(o => o.Path.ToLower().Trim('/').Equals(context.HttpContext.Request.Path.ToString().ToLower().Trim('/')) && o.Code.Equals(code));

                if (permission == null)
                {
                    ResponseBase response = new ResponseBase();
                    response.Success = false;
                    response.Code = HttpStatusCode.Unauthorized;
                    response.Message = "用户权限不足";
                    context.Result = new JsonResult(response);
                }


            }

        }

    }
}
