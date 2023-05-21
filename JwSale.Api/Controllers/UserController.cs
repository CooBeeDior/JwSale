using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Response.User;
using JwSale.Model.Dto.Response.UserRole;
using JwSale.Model.Enums;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Attributes;
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
    public class UserController : ManageControllerBase
    {
        private readonly IUserService _userService;
        private IDistributedCache _cache;

        private JwSaleOptions jwSaleOptions;


        public UserController(JwSaleDbContext context, IUserService userService, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions) : base(context)
        {
            this._userService = userService;
            this._cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;

        }

        /// <summary>
        /// 登录获取令牌（填入Authorize）
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [MoudleInfo("登录", false)]
        [NoAuthRequired]
        [HttpPost("api/User/Login")]
        public async Task<ActionResult<ResponseBase<LoginResponse>>> Login(LoginRequest login)
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
                        response.Code = HttpStatusCode.Unauthorized;
                        response.Message = "用户被禁用，请联系管理员开启";
                    }
                    else if (userinfo.ExpiredTime == null || userinfo.ExpiredTime < DateTime.Now)
                    {
                        response.Success = false;
                        response.Code = HttpStatusCode.Unauthorized;
                        response.Message = "用户已过期";
                    }
                    else
                    {
                        UserToken userToken = new UserToken()
                        {
                            UserId = userinfo.Id,
                            UserName = userinfo.UserName,
                            Ip = HttpContext.Connection.RemoteIpAddress.ToString(),
                            Expireds = 60 * 60 * 24 * 30 * 3,
                            AddTime = DateTime.Now
                        };
                        string token = UserHelper.GenerateToken(userToken, jwSaleOptions.TokenKey);

                        var permissions = await _userService.GetPermissions(userinfo.Id);

                        LoginResponse loginResponse = new LoginResponse()
                        {
                            Token = token,
                            ExpiredTime = userinfo.ExpiredTime,
                            UserInfo = userinfo,
                            Permissions = permissions
                        };

                        UserCache userCache = new UserCache()
                        {
                            Token = token,
                            ExpiredTime = userToken.AddTime.AddSeconds(userToken.Expireds),
                            UserInfo = userinfo,
                            Permissions = permissions,
                            LoginDevice = HttpContext.LoginDevice(),
                            LoginTime = DateTime.Now
                        };

                        var cacheEntryOptions = new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(userToken.Expireds) };
                        await _cache.SetStringAsync(CacheKeyHelper.GetLoginUserKey(userinfo.UserName, HttpContext.LoginDevice()), userCache.ToJson(), cacheEntryOptions);
                        response.Data = loginResponse;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Code = HttpStatusCode.Unauthorized;
                    response.Message = "密码错误";
                }
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.Unauthorized;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }




        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取用户信息")]
        [HttpPost("api/User/GetUserInfo")]
        public async Task<ActionResult<ResponseBase<LoginResponse>>> GetUserInfo()
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();

            var userinfo = DbContext.UserInfos.Where(o => o.Id == UserInfo.Id).FirstOrDefault();
            if (userinfo != null)
            {
                response.Data = userinfo;
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取角色权限")]
        [HttpPost("api/User/GetUserPermission")]
        public async Task<ActionResult<ResponseBase<BriefInfo>>> GetUserPermission()
        {
            ResponseBase<IList<BriefInfo>> response = new ResponseBase<IList<BriefInfo>>();

            var permissions = await _userService.GetPermissions(UserInfo.Id);
            response.Data = permissions;
            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/User/AddUser")]
        [MoudleInfo("添加用户")]
        public async Task<ActionResult<ResponseBase<UserInfo>>> AddUser(AddUserRequest addUser)
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            if (DbContext.UserInfos.Where(o => o.UserName == addUser.UserName).Any())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"{addUser.UserName}用户名已存在";
            }
            else
            {
                UserInfo userInfo = new UserInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = addUser.UserName,
                    Password = addUser.Password.ToMd5(),
                    RealName = addUser.RealName,
                    RealNamePin = addUser.RealName?.ToPinYin(),

                    Phone = addUser.Phone,
                    IdCard = addUser.IdCard,
                    BirthDay = addUser.BirthDay,
                    Province = addUser.Province,
                    City = addUser.City,
                    Area = addUser.Area,
                    Address = addUser.Address,
                    Remark = addUser.Remark,
                    Qq = addUser.Qq,
                    WxNo = addUser.WxNo,
                    TelPhone = addUser.TelPhone,
                    PositionName = addUser.PositionName,
                    HeadImageUrl = addUser.HeadImageUrl,

                    Type = addUser.Type,
                    ExpiredTime = addUser.ExpiredTime?.Date ?? DateTime.Now.AddYears(1),



                    AddTime = DateTime.Now,
                    AddUserId = UserInfo.Id,
                    AddUserRealName = UserInfo.RealName,
                    UpdateTime = DateTime.Now,
                    UpdateUserId = UserInfo.Id,
                    UpdateUserRealName = UserInfo.RealName,
                };
                DbContext.Add(userInfo);



                await DbContext.SaveChangesAsync();

                response.Data = userInfo;
            }




            LoginResponse loginResponse = new LoginResponse();
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 设置所有权限
        /// </summary>
        /// <param name="setAllPermisson"></param>
        /// <returns></returns>
        [HttpPost("api/User/SetAllPermisson")]
        [MoudleInfo("设置所有权限")]
        public async Task<ActionResult<ResponseBase>> SetAllPermisson(SetAllPermissonRequest setAllPermisson)
        {
            ResponseBase<IList<BriefInfo>> response = new ResponseBase<IList<BriefInfo>>();
            if (!DbContext.UserInfos.Where(o => o.Id == setAllPermisson.UserId).Any())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"{setAllPermisson.UserId}用户名不存在";
            }
            else
            {
                await _userService.SetUserAllPermission(setAllPermisson.UserId);
                response.Data = await _userService.GetPermissions(setAllPermisson.UserId);

            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="getUsers"></param>
        /// <returns></returns>
        [HttpPost("api/User/GetUsers")]
        [MoudleInfo("获取用户列表")]
        public async Task<ActionResult<PageResponseBase<IEnumerable<UserInfo>>>> GetUsers(GetUsersRequest getUsers)
        {
            PageResponseBase<IEnumerable<UserInfo>> response = new PageResponseBase<IEnumerable<UserInfo>>();
            var userinfos = DbContext.UserInfos.AsEnumerable();
            if (!string.IsNullOrEmpty(getUsers.Name))
            {
                userinfos = userinfos.Where(o => o.UserName.Contains(getUsers.Name) || o.RealName?.Contains(getUsers.Name) == true || o.RealNamePin?.ToLower()?.Contains(getUsers.Name.ToLower()) == true);
            }
            if (getUsers.Status != null)
            {
                userinfos = userinfos.Where(o => o.Status == getUsers.Status);
            }
            response.TotalCount = userinfos.Count();
            userinfos = userinfos.OrderBy(getUsers.OrderBys).ToPage(getUsers.PageIndex, getUsers.PageSize);
            response.Data = userinfos;

            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="setUserStatus"></param>
        /// <returns></returns>
        [HttpPost("api/User/SetUserStatus")]
        [MoudleInfo("设置用户状态")]
        public async Task<ActionResult<ResponseBase>> SetUserStatus(SetUserStatusRequest setUserStatus)
        {
            ResponseBase response = new ResponseBase();
            var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == setUserStatus.UserId).FirstOrDefault();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户不存在";
            }
            else
            {
                userinfo.Status = setUserStatus.Status;
                userinfo.UpdateUserId = UserInfo.UpdateUserId;
                userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
                userinfo.UpdateTime = DateTime.Now;
                await DbContext.SaveChangesAsync();

            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="resetUserPwd"></param>
        /// <returns></returns>
        [HttpPost("api/User/ResetUserPwd")]
        [MoudleInfo("重置用户密码")]
        public async Task<ActionResult<ResponseBase<string>>> ResetUserPwd(ResetUserPwdRequest resetUserPwd)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == UserInfo.Id).FirstOrDefault();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户不存在";
            }
            else
            {
                userinfo.Password = resetUserPwd.NewPassword.ToMd5();
                userinfo.UpdateUserId = UserInfo.UpdateUserId;
                userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
                userinfo.UpdateTime = DateTime.Now;
                await DbContext.SaveChangesAsync();


                //await cache.RemoveAsync(CacheKeyHelper.GetUserTokenKey(userinfo.UserName));
                response.Data = userinfo.Password;

            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 修改用户简介
        /// </summary>
        /// <param name="setUserProfile"></param>
        /// <returns></returns>
        [HttpPost("api/User/SetUserProfile")]
        [MoudleInfo("修改用户简介")]
        public async Task<ActionResult<ResponseBase<UserInfo>>> SetUserProfile(SetUserProfileRequest setUserProfile)
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == UserInfo.Id).FirstOrDefault();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户不存在";
            }
            else
            {
                userinfo.Phone = setUserProfile.Phone;
                userinfo.Email = setUserProfile.Email;
                userinfo.RealName = setUserProfile.RealName;
                userinfo.RealNamePin = setUserProfile.RealName?.ToPinYin();
                userinfo.Qq = setUserProfile.Qq;
                userinfo.WxNo = setUserProfile.WxNo;
                userinfo.TelPhone = setUserProfile.TelPhone;
                userinfo.PositionName = setUserProfile.PositionName;
                userinfo.Province = setUserProfile.Province;
                userinfo.City = setUserProfile.City;
                userinfo.Area = setUserProfile.Area;
                userinfo.Address = setUserProfile.Address;
                userinfo.HeadImageUrl = setUserProfile.HeadImageUrl;
                userinfo.Remark = setUserProfile.Remark;

                userinfo.UpdateUserId = UserInfo.UpdateUserId;
                userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
                userinfo.UpdateTime = DateTime.Now;
                await DbContext.SaveChangesAsync();

                response.Data = userinfo;

            }
            return await response.ToJsonResultAsync();

        }



        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="setUserAuth"></param>
        /// <returns></returns>
        [HttpPost("api/User/SetUserAuth")]
        [MoudleInfo("修改用户授权")]
        public async Task<ActionResult<ResponseBase<UserInfo>>> SetUserAuth(SetUserAuthRequest setUserAuth)
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == setUserAuth.UserId).FirstOrDefault();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户不存在";
            }
            else
            {

                userinfo.Type = setUserAuth.Type;
                userinfo.ExpiredTime = setUserAuth.ExpiredTime?.Date ?? DateTime.Now.AddYears(1);


                userinfo.UpdateUserId = UserInfo.UpdateUserId;
                userinfo.UpdateUserRealName = UserInfo.UpdateUserRealName;
                userinfo.UpdateTime = DateTime.Now;
                await DbContext.SaveChangesAsync();

                response.Data = userinfo;

            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("api/User/DeleteUser")]
        [MoudleInfo("删除用户")]
        public async Task<ActionResult<ResponseBase<UserInfo>>> DeleteUser(string id)
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();
            if (string.IsNullOrWhiteSpace(id))
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "id不能为空";
                return response;
            }
            var userinfo = DbContext.UserInfos.AsEnumerable().Where(o => o.Id == id).FirstOrDefault();
 
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户不存在";
            }
            else
            {
                DbContext.UserInfos.Remove(userinfo);        
                await DbContext.SaveChangesAsync();
                response.Data = userinfo;

            }
            return await response.ToJsonResultAsync();

        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("退出", false)]
        [HttpPost("api/User/Logout")]
        public async Task<ActionResult> Logout()
        {
            ResponseBase response = new ResponseBase();
            var loginDevice = HttpContext.LoginDevice();
            await _cache.RemoveAsync(CacheKeyHelper.GetLoginUserKey(UserInfo.UserName, loginDevice));
            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 获取用户功能列表
        /// </summary> 
        /// <returns></returns>
        [MoudleInfo("获取用户功能列表", true)]
        [HttpPost("api/UserRole/GetUserFunctions")]
        public async Task<ActionResult<ResponseBase<IList<FunctionTreeResponse>>>> GetUserFunctions()
        {
            ResponseBase<IList<FunctionTreeResponse>> response = new ResponseBase<IList<FunctionTreeResponse>>();

            response.Data = await _userService.GetUserFunctions(UserInfo.Id);
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 解绑微信小程序
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取用户功能列表", true)]
        [HttpPost("api/User/UnBindWechatUser")]
        public async Task<ActionResult<ResponseBase<IList<FunctionTreeResponse>>>> UnBindWechatUser(UnBindWechatUserRequest request)
        {
            ResponseBase<IList<FunctionTreeResponse>> response = new ResponseBase<IList<FunctionTreeResponse>>();

            var userinfo = await DbContext.UserInfos.Where(o => o.Id == request.UserId && o.WxOpenId == request.WxOpenId).FirstOrDefaultAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.NotFound;
                response.Message = "用户不存在";
            }
            else
            {
                userinfo.WxOpenId = null;
                userinfo.WxUnionId = null;
                await DbContext.SaveChangesAsync();
            }
            return await response.ToJsonResultAsync();
        }




    }
}