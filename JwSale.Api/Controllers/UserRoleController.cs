using JwSale.Api.Filters;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Response.UserRole;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JwSale.Model;
using JwSale.Api.Extensions;
using JwSale.Model.Enums;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 角色权限管理
    /// </summary>
    [MoudleInfo("角色权限管理", 2)]
    public class UserRoleController : JwSaleControllerBase
    {
        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private IHttpContextAccessor accessor;
        public UserRoleController(JwSaleDbContext context, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            this.accessor = accessor;

        }







        /// <summary>
        /// 获取用户功能列表
        /// </summary> 
        /// <returns></returns>
        [MoudleInfo("获取用户功能列表", true)]
        [HttpPost("api/UserRole/GetUserFunctions")]
        public async Task<ActionResult<ResponseBase<IList<FunctionTree>>>> GetUserFunctions()
        {
            ResponseBase<IList<FunctionTree>> response = new ResponseBase<IList<FunctionTree>>();

            var functions = DbContext.FunctionInfos.OrderBy(o => o.Order).AsEnumerable();
            var permissions = await (
                        from u in DbContext.UserInfos.AsNoTracking()
                        join ur in DbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
                        join r in DbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
                        join rp in DbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
                        join f in DbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
                        where u.Id == UserInfo.Id
                        select new BriefInfoWithId()
                        {
                            Id = f.Id,
                            Code = f.Code,
                            Name = f.Name
                        }).Union(
                            from u in DbContext.UserInfos.AsNoTracking()
                            join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                            join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                            where u.Id == UserInfo.Id && up.Type == (short)PermissionType.Increase
                            select new BriefInfoWithId()
                            {
                                Id = f.Id,
                                Code = f.Code,
                                Name = f.Name
                            }).Except(
                              from u in DbContext.UserInfos.AsNoTracking()
                              join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                              join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                              where u.Id == UserInfo.Id && up.Type == (short)PermissionType.Decut
                              select new BriefInfoWithId()
                              {
                                  Id = f.Id,
                                  Code = f.Code,
                                  Name = f.Name
                              }).ToListAsync();
            FunctionTree functionTree = new FunctionTree()
            {
                Id = Guid.Empty,
                Code = "Root",
                Name = "根节点"
            };
            getfuntions(functions, functionTree, permissions);

            response.Data = functionTree.Tree;
            return await response.ToJsonResultAsync();
        }


 




        private void getfuntions(IEnumerable<FunctionInfo> functions, FunctionTree functionTree, IList<BriefInfoWithId> permissions)
        {
            var filterFunctions = functions.Where(o => o.ParentId == functionTree.Id).Select(o => new FunctionTree { Id = o.Id, Name = o.Name, Code = o.Code }).ToList();
            functionTree.Tree = filterFunctions;
            foreach (var item in filterFunctions)
            {
                if (permissions.Any(o => o.Id == item.Id))
                {
                    item.IsPermission = true;
                }
                getfuntions(functions, item, permissions);
            }
        }



    }
}