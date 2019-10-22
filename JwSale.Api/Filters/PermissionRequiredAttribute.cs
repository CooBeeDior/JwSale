using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Enums;
using JwSale.Packs.Attributes;
using JwSale.Repository.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (controllerActionDescriptor != null)
                {
                    var controllerMoudleInfo = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(inherit: true).FirstOrDefault(a => a.GetType().Equals(typeof(MoudleInfoAttribute)));
                    var actionMoudleInfo = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(MoudleInfoAttribute)));

                }
                var permission = getPermissions(userinfo.Id);
            }
    
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<IList<BriefInfo>> getPermissions(Guid userId)
        {
            return await (
                 from u in jwSaleDbContext.UserInfos.AsNoTracking()
                 join ur in jwSaleDbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
                 join r in jwSaleDbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
                 join rp in jwSaleDbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
                 join f in jwSaleDbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
                 where u.Id == userId
                 select new BriefInfo()
                 {
                     Code = f.Code,
                     Name = f.Name
                 }).Union(
                     from u in jwSaleDbContext.UserInfos.AsNoTracking()
                     join up in jwSaleDbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                     join f in jwSaleDbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                     where u.Id == userId && up.Type == (short)PermissionType.Increase
                     select new BriefInfo()
                     {
                         Code = f.Code,
                         Name = f.Name
                     }).Except(
                       from u in jwSaleDbContext.UserInfos.AsNoTracking()
                       join up in jwSaleDbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                       join f in jwSaleDbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                       where u.Id == userId && up.Type == (short)PermissionType.Decut
                       select new BriefInfo()
                       {
                           Code = f.Code,
                           Name = f.Name
                       }).ToListAsync();
        }
    }
}
