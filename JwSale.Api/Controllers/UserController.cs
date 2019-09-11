using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Response.User;
using JwSale.Model.Dto.Response.UserRole;
using JwSale.Model.Enums;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [MoudleInfo("用户管理", 1)]
    public class UserController : JwSaleControllerBase
    {
        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private IHttpContextAccessor accessor;
        public UserController(JwSaleDbContext context, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            this.accessor = accessor;

        }

        /// <summary>
        /// 登录获取令牌（填入Authorize）
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [MoudleInfo("登录", false)]
        [NoPermissionRequired]
        [HttpPost("api/User/Login")]
        public async Task<ActionResult<ResponseBase<LoginResponse>>> Login(Login login)
        {
            ResponseBase<LoginResponse> response = new ResponseBase<LoginResponse>();

            var userinfo = DbContext.UserInfos.Where(o => o.UserName == login.UserName).FirstOrDefault();
            if (userinfo != null)
            {
                if (userinfo.Password.Equals(login.Password.ToMd5(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (userinfo.Status != 0)
                    {
                        response.Success = false;
                        response.Code = HttpStatusCode.BadRequest;
                        response.Message = "用户被禁用，请联系管理员开启";
                    }
                    else
                    {
                        UserToken userToken = new UserToken()
                        {
                            UserId = userinfo.Id,
                            UserName = userinfo.UserName,
                            Ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                            Expireds = 60 * 60 * 24 * 7,
                            AddTime = DateTime.Now
                        };
                        string token = UserHelper.GenerateToken(userToken, jwSaleOptions.TokenKey);

                        var permissions = await getPermissions(userinfo.Id);

                        LoginResponse loginResponse = new LoginResponse()
                        {
                            Token = token,
                            ExpiredTime = userToken.AddTime.AddSeconds(userToken.Expireds),
                            UserInfo = userinfo,
                            Permissions = permissions
                        };


                        UserCache userCache = new UserCache()
                        {
                            Token = token,
                            ExpiredTime = userToken.AddTime.AddSeconds(userToken.Expireds),
                            UserInfo = userinfo,
                        };

                        var cacheEntryOptions = new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(userToken.Expireds) };
                        await cache.SetStringAsync(CacheKeyHelper.GetUserTokenKey(userinfo.UserName), userCache.ToJson(), cacheEntryOptions);
                        response.Data = loginResponse;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Code = HttpStatusCode.BadRequest;
                    response.Message = "密码错误";
                }
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }

        ///// <summary>
        ///// 获取用户信息
        ///// </summary>
        ///// <returns></returns>
        //[MoudleInfo("获取用户信息", false)]
        //[HttpPost("api/User/GetUserInfo")]
        //public async Task<ActionResult<ResponseBase<LoginResponse>>> GetUserInfo()
        //{
        //    ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();

        //    var userinfo = DbContext.UserInfos.Where(o => o.UserName == UserInfo.UserName).FirstOrDefault();
        //    if (userinfo != null)
        //    {
        //        response.Data = userinfo;
        //    }
        //    else
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.NotFound;
        //        response.Message = "用户名不存在";
        //    }
        //    return await response.ToJsonResultAsync();
        //}

        ///// <summary>
        ///// 获取角色权限
        ///// </summary>
        ///// <returns></returns>
        //[MoudleInfo("获取角色权限", false)]
        //[HttpPost("api/User/GetUserPermission")]
        //public async Task<ActionResult<ResponseBase<LoginResponse>>> GetUserPermission()
        //{
        //    ResponseBase<IList<BriefInfo>> response = new ResponseBase<IList<BriefInfo>>();

        //    var permissions = await getPermissions(UserInfo.Id);
        //    response.Data = permissions;
        //    return await response.ToJsonResultAsync();
        //}



        ///// <summary>
        ///// 添加用户
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("api/User/AddUser")]
        //[MoudleInfo("添加用户")]
        //public async Task<ActionResult<ResponseBase<UserInfo>>> AddUser(AddUser addUser)
        //{
        //    ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
        //    if (DbContext.UserInfos.Where(o => o.UserName == addUser.UserName).Any())
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.BadRequest;
        //        response.Message = $"{addUser.UserName}用户名已存在";
        //    }
        //    else
        //    {
        //        UserInfo userInfo = new UserInfo()
        //        {
        //            Id = Guid.NewGuid(),
        //            UserName = addUser.UserName,
        //            Password = addUser.Password.ToMd5(),
        //            RealName = addUser.RealName,
        //            RealNamePin = addUser.RealName?.ToPinYin(),

        //            Phone = addUser.Phone,
        //            Province = addUser.Province,
        //            City = addUser.City,
        //            Area = addUser.Area,
        //            Address = addUser.Address,
        //            Remark = addUser.Remark,
        //            Qq = addUser.Qq,
        //            WxNo = addUser.WxNo,
        //            TelPhone = addUser.TelPhone,
        //            PositionName = addUser.PositionName,
        //            HeadImageUrl = addUser.HeadImageUrl,

        //            Type = addUser.Type,
        //            ExpiredTime = addUser.ExpiredTime,
        //            WxCount = addUser.WxCount,


        //            AddTime = DateTime.Now,
        //            AddUserId = UserInfo.Id,
        //            AddUserRealName = UserInfo.RealName,
        //            UpdateTime = DateTime.Now,
        //            UpdateUserId = UserInfo.Id,
        //            UpdateUserRealName = UserInfo.RealName,
        //        };
        //        DbContext.Add(userInfo);



        //        await DbContext.SaveChangesAsync();

        //        response.Data = userInfo;
        //    }




        //    LoginResponse loginResponse = new LoginResponse();
        //    return await response.ToJsonResultAsync();
        //}


        ///// <summary>
        ///// 设置所有权限
        ///// </summary>
        ///// <param name="setAllPermisson"></param>
        ///// <returns></returns>
        //[HttpPost("api/User/SetAllPermisson")]
        //[MoudleInfo("设置所有权限")]
        //public async Task<ActionResult<ResponseBase<UserInfo>>> SetAllPermisson(SetAllPermisson setAllPermisson)
        //{
        //    ResponseBase<IList<BriefInfo>> response = new ResponseBase<IList<BriefInfo>>();
        //    if (!DbContext.UserInfos.Where(o => o.Id == setAllPermisson.UserId).Any())
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.BadRequest;
        //        response.Message = $"{setAllPermisson.UserId}用户名不存在";
        //    }
        //    else
        //    {


        //        var functionInfos = await DbContext.FunctionInfos.ToListAsync();

        //        await DbContext.UserPermissionInfos.Where(o => o.UserId == setAllPermisson.UserId).DeleteAsync();


        //        IList<UserPermissionInfo> userPermissionInfos = new List<UserPermissionInfo>();


        //        foreach (var funciton in functionInfos)
        //        {
        //            UserPermissionInfo userPermissionInfo = new UserPermissionInfo()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserId = setAllPermisson.UserId,
        //                FunctionId = funciton.Id,
        //                Type = 0,

        //                AddTime = DateTime.Now,
        //                AddUserId = UserInfo.Id,
        //                AddUserRealName = UserInfo.RealName,
        //                UpdateTime = DateTime.Now,
        //                UpdateUserId = UserInfo.Id,
        //                UpdateUserRealName = UserInfo.RealName,
        //            };
        //            userPermissionInfos.Add(userPermissionInfo);
        //        }
        //        await DbContext.AddRangeAsync(userPermissionInfos);

        //        await DbContext.SaveChangesAsync();

        //        response.Data = await getPermissions(setAllPermisson.UserId);


        //    }
        //    return await response.ToJsonResultAsync();

        //}

        ///// <summary>
        ///// 获取用户列表
        ///// </summary>
        ///// <param name="getUsers"></param>
        ///// <returns></returns>
        //[HttpPost("api/User/GetUsers")]
        //[MoudleInfo("获取用户列表")]
        //public async Task<ActionResult<PageResponseBase<IEnumerable<UserInfo>>>> GetUsers(GetUsers getUsers)
        //{
        //    PageResponseBase<IEnumerable<UserInfo>> response = new PageResponseBase<IEnumerable<UserInfo>>();
        //    var userinfos = DbContext.UserInfos.AsEnumerable();
        //    if (!string.IsNullOrEmpty(getUsers.Name))
        //    {
        //        userinfos = userinfos.Where(o => o.UserName.Contains(getUsers.Name) || o.RealName?.Contains(getUsers.Name) == true || o.RealNamePin?.ToLower()?.Contains(getUsers.Name.ToLower()) == true);
        //    }
        //    if (getUsers.Status != null)
        //    {
        //        userinfos = userinfos.Where(o => o.Status == getUsers.Status);
        //    }
        //    response.TotalCount = userinfos.Count();
        //    userinfos = userinfos.OrderBy(getUsers.OrderBys).ToPage(getUsers.PageIndex, getUsers.PageSize);
        //    response.Data = userinfos;

        //    return await response.ToJsonResultAsync();
        //}


        ///// <summary>
        ///// 设置用户状态
        ///// </summary>
        ///// <param name="setUserStatus"></param>
        ///// <returns></returns>
        //[HttpPost("api/User/SetUserStatus")]
        //[MoudleInfo("设置用户状态")]
        //public async Task<ActionResult<ResponseBase>> SetUserStatus(SetUserStatus setUserStatus)
        //{
        //    ResponseBase response = new ResponseBase();
        //    var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == setUserStatus.UserId).FirstOrDefault();
        //    if (userinfo == null)
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.NotFound;
        //        response.Message = "用户不存在";
        //    }
        //    else
        //    {
        //        userinfo.Status = setUserStatus.Status;
        //        userinfo.UpdateUserId = UserInfo.UpdateUserId;
        //        userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
        //        userinfo.UpdateTime = DateTime.Now;
        //        await DbContext.SaveChangesAsync();

        //    }
        //    return await response.ToJsonResultAsync();
        //}

        ///// <summary>
        ///// 重置用户密码
        ///// </summary>
        ///// <param name="resetUserPwd"></param>
        ///// <returns></returns>
        //[HttpPost("api/User/ResetUserPwd")]
        //[MoudleInfo("重置用户密码")]
        //public async Task<ActionResult<ResponseBase<string>>> ResetUserPwd(ResetUserPwd resetUserPwd)
        //{
        //    ResponseBase<string> response = new ResponseBase<string>();
        //    var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == resetUserPwd.UserId).FirstOrDefault();
        //    if (userinfo == null)
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.NotFound;
        //        response.Message = "用户不存在";
        //    }
        //    else
        //    {
        //        userinfo.Password = resetUserPwd.Password.ToMd5();
        //        userinfo.UpdateUserId = UserInfo.UpdateUserId;
        //        userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
        //        userinfo.UpdateTime = DateTime.Now;
        //        await DbContext.SaveChangesAsync();


        //        await cache.RemoveAsync(CacheKeyHelper.GetUserTokenKey(userinfo.UserName));
        //        response.Data = resetUserPwd.Password;

        //    }
        //    return await response.ToJsonResultAsync();

        //}

        ///// <summary>
        ///// 修改用户简介
        ///// </summary>
        ///// <param name="setUserProfile"></param>
        ///// <returns></returns>
        //[HttpPost("api/User/SetUserProfile")]
        //[MoudleInfo("修改用户简介")]
        //public async Task<ActionResult<ResponseBase<UserInfo>>> SetUserProfile(SetUserProfile setUserProfile)
        //{
        //    ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
        //    var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == setUserProfile.UserId).FirstOrDefault();
        //    if (userinfo == null)
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.NotFound;
        //        response.Message = "用户不存在";
        //    }
        //    else
        //    {
        //        userinfo.Phone = setUserProfile.Phone;
        //        userinfo.Email = setUserProfile.Email;
        //        userinfo.RealName = setUserProfile.RealName;
        //        userinfo.RealNamePin = setUserProfile.RealName?.ToPinYin();
        //        userinfo.Qq = setUserProfile.Qq;
        //        userinfo.WxNo = setUserProfile.WxNo;
        //        userinfo.TelPhone = setUserProfile.TelPhone;
        //        userinfo.PositionName = setUserProfile.PositionName;
        //        userinfo.Province = setUserProfile.Province;
        //        userinfo.City = setUserProfile.City;
        //        userinfo.Area = setUserProfile.Area;
        //        userinfo.Address = setUserProfile.Address;
        //        userinfo.HeadImageUrl = setUserProfile.HeadImageUrl;
        //        userinfo.Remark = setUserProfile.Remark;

        //        userinfo.UpdateUserId = UserInfo.UpdateUserId;
        //        userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
        //        userinfo.UpdateTime = DateTime.Now;
        //        await DbContext.SaveChangesAsync();

        //        response.Data = userinfo;

        //    }
        //    return await response.ToJsonResultAsync();

        //}



        ///// <summary>
        ///// 修改用户授权
        ///// </summary>
        ///// <param name="setUserAuth"></param>
        ///// <returns></returns>
        //[HttpPost("api/User/SetUserAuth")]
        //[MoudleInfo("修改用户授权")]
        //public async Task<ActionResult<ResponseBase<UserInfo>>> SetUserAuth(SetUserAuth setUserAuth)
        //{
        //    ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
        //    var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == setUserAuth.UserId).FirstOrDefault();
        //    if (userinfo == null)
        //    {
        //        response.Success = false;
        //        response.Code = HttpStatusCode.NotFound;
        //        response.Message = "用户不存在";
        //    }
        //    else
        //    {

        //        userinfo.Type = setUserAuth.Type;
        //        userinfo.ExpiredTime = setUserAuth.ExpiredTime;
        //        userinfo.WxCount = setUserAuth.WxCount;

        //        userinfo.UpdateUserId = UserInfo.UpdateUserId;
        //        userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
        //        userinfo.UpdateTime = DateTime.Now;
        //        await DbContext.SaveChangesAsync();

        //        response.Data = userinfo;

        //    }
        //    return await response.ToJsonResultAsync();

        //}


        ///// <summary>
        ///// 登出
        ///// </summary>
        ///// <returns></returns>
        //[MoudleInfo("退出", false)]
        //[NoPermissionRequired]
        //[HttpPost("api/User/Logout")]
        //public async Task<ActionResult> Logout()
        //{
        //    ResponseBase response = new ResponseBase();

        //    await cache.RemoveAsync(CacheKeyHelper.GetUserTokenKey(UserInfo.UserName));
        //    return await response.ToJsonResultAsync();
        //}



        ///// <summary>
        ///// 获取用户功能列表
        ///// </summary> 
        ///// <returns></returns>
        //[MoudleInfo("获取用户功能列表", true)]
        //[HttpPost("api/UserRole/GetUserFunctions")]
        //public async Task<ActionResult<ResponseBase<IList<FunctionTree>>>> GetUserFunctions()
        //{
        //    ResponseBase<IList<FunctionTree>> response = new ResponseBase<IList<FunctionTree>>();

        //    var functions = DbContext.FunctionInfos.OrderBy(o => o.Order).AsEnumerable();
        //    var permissions = await (
        //                from u in DbContext.UserInfos.AsNoTracking()
        //                join ur in DbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
        //                join r in DbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
        //                join rp in DbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
        //                join f in DbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
        //                where u.Id == UserInfo.Id
        //                select new BriefInfoWithId()
        //                {
        //                    Id = f.Id,
        //                    Code = f.Code,
        //                    Name = f.Name
        //                }).Union(
        //                    from u in DbContext.UserInfos.AsNoTracking()
        //                    join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
        //                    join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
        //                    where u.Id == UserInfo.Id && up.Type == (short)PermissionType.Increase
        //                    select new BriefInfoWithId()
        //                    {
        //                        Id = f.Id,
        //                        Code = f.Code,
        //                        Name = f.Name
        //                    }).Except(
        //                      from u in DbContext.UserInfos.AsNoTracking()
        //                      join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
        //                      join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
        //                      where u.Id == UserInfo.Id && up.Type == (short)PermissionType.Decut
        //                      select new BriefInfoWithId()
        //                      {
        //                          Id = f.Id,
        //                          Code = f.Code,
        //                          Name = f.Name
        //                      }).ToListAsync();
        //    FunctionTree functionTree = new FunctionTree()
        //    {
        //        Id = Guid.Empty,
        //        Code = "Root",
        //        Name = "根节点"
        //    };
        //    getfuntions(functions, functionTree, permissions);

        //    response.Data = functionTree.Tree;
        //    return await response.ToJsonResultAsync();
        //}







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



        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<IList<BriefInfo>> getPermissions(Guid userId)
        {
            return await (
                 from u in DbContext.UserInfos.AsNoTracking()
                 join ur in DbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
                 join r in DbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
                 join rp in DbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
                 join f in DbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
                 where u.Id == userId
                 select new BriefInfo()
                 {
                     Code = f.Code,
                     Name = f.Name
                 }).Union(
                     from u in DbContext.UserInfos.AsNoTracking()
                     join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                     join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                     where u.Id == userId && up.Type == (short)PermissionType.Increase
                     select new BriefInfo()
                     {
                         Code = f.Code,
                         Name = f.Name
                     }).Except(
                       from u in DbContext.UserInfos.AsNoTracking()
                       join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                       join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                       where u.Id == userId && up.Type == (short)PermissionType.Decut
                       select new BriefInfo()
                       {
                           Code = f.Code,
                           Name = f.Name
                       }).ToListAsync();
        }





    }
}
