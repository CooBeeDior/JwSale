using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Packs.Attributes;
using JwSale.Repository.Context;
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
    public class PermissionRequiredAttribute : Attribute, IAuthorizationFilter
    {
        private IDistributedCache cache;
        private JwSaleDbContext jwSaleDbContext;
        public PermissionRequiredAttribute(JwSaleDbContext jwSaleDbContext, IDistributedCache cache)
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
                }
                var authRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<AuthRequiredAttribute>(true);
                if (authRequiredAttribute != null)
                {
                    isAuth = true;
                }

                if (!isAuth)
                {
                    var noAuthRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<NoAuthRequiredAttribute>(true);
                    if (noAuthRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                    noAuthRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<NoAuthRequiredAttribute>(true);
                    if (noAuthRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                    var noPermissionRequiredAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<NoPermissionRequiredAttribute>(true);
                    if (noPermissionRequiredAttribute != null)
                    {
                        isAuth = false;
                    }
                    noPermissionRequiredAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<NoPermissionRequiredAttribute>(true);
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
            var userinfo = context.HttpContext.Items[CacheKeyHelper.GetHttpContextUserKey()] as UserInfo;
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

                var userTokenCache = cache.GetString(CacheKeyHelper.GetUserTokenKey(userinfo.UserName))?.ToObj<UserCache>();

                string code = string.IsNullOrEmpty(controllerMoudleInfo.Code) ? controllerMoudleInfo.Name.ToPinYin() : controllerMoudleInfo.Code;
                Guid parentId = userTokenCache?.Permissions?.FirstOrDefault(o => o.Code.Equals(code))?.Id ?? Guid.Empty;
                code = string.IsNullOrEmpty(actionMoudleInfo.Code) ? actionMoudleInfo.Name.ToPinYin() : actionMoudleInfo.Code;
                var permission = userTokenCache?.Permissions?.FirstOrDefault(o => o.ParentId.Equals(parentId) && o.Code.Equals(code));

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
