using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Response.User;
using JwSale.Model.Dto.Response.UserRole;
using JwSale.Packs.Options;
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


        public UserController(IUserService userService, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions)
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
        [HttpPost("api/user/login")]
        public async Task<ActionResult<ResponseBase>> Login(LoginRequest login)
        {
            ResponseBase<LoginResponse> response = new ResponseBase<LoginResponse>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.UserName == login.UserName).ToOneAsync();
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
                            ExpiredTime = userinfo.ExpiredTime,
                            AddTime = DateTime.Now
                        };
                        string token = userToken.GenerateToken(jwSaleOptions.TokenKey);

                        var permissions = await _userService.GetUserPermissions(userinfo.Id);

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
                            UserInfo = userinfo,
                            Permissions = permissions,
                            LoginDevice = HttpContext.LoginDevice(),
                            LoginTime = DateTime.Now
                        };

                        var cacheEntryOptions = new DistributedCacheEntryOptions() { AbsoluteExpiration = new DateTimeOffset(userinfo.ExpiredTime) };
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
        /// 登出
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("退出", false)]
        [HttpPost("api/user/logout")]
        public async Task<ActionResult<ResponseBase>> Logout()
        {
            ResponseBase response = new ResponseBase();
            var loginDevice = HttpContext.LoginDevice();
            await _cache.RemoveAsync(CacheKeyHelper.GetLoginUserKey(CurrentUserInfo.UserName, loginDevice));
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取用户信息")]
        [HttpPost("api/user/getuserinfo")]
        public async Task<ActionResult<ResponseBase>> GetUserInfo()
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();

            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == CurrentUserInfo.Id).ToOneAsync();
            if (userinfo != null)
            {
                response.Data = userinfo;
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }
        /// <summary>
        /// 获取用户功能列表
        /// </summary> 
        /// <returns></returns>
        [MoudleInfo("获取用户功能列表", true)]
        [HttpPost("api/userrole/getuserfunctions")]
        public async Task<ActionResult<ResponseBase>> GetUserFunctions()
        {
            ResponseBase<IList<FunctionTreeResponse>> response = new ResponseBase<IList<FunctionTreeResponse>>();

            response.Data = await _userService.GetUserPermissionTree(CurrentUserInfo.Id);
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("添加角色", false)]
        [HttpPost("api/user/addrole")]
        public async Task<ActionResult> AddRole()
        {
            ResponseBase response = new ResponseBase();

            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("修改角色", false)]
        [HttpPost("api/user/updaterole")]
        public async Task<ActionResult> UpdateRole()
        {
            ResponseBase response = new ResponseBase();


            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("删除角色", false)]
        [HttpPost("api/user/deleterole")]
        public async Task<ActionResult> DeleteRole()
        {
            ResponseBase response = new ResponseBase();


            return await response.ToJsonResultAsync();
        }




        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取角色权限")]
        [HttpPost("api/user/getuserpermission")]
        public async Task<ActionResult<ResponseBase<BriefInfo>>> GetUserPermission()
        {
            ResponseBase<IList<BriefInfo>> response = new ResponseBase<IList<BriefInfo>>();

            var permissions = await _userService.GetUserPermissions(CurrentUserInfo.Id);
            response.Data = permissions;
            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/user/adduser")]
        [MoudleInfo("添加用户")]
        public async Task<ActionResult<ResponseBase>> AddUser(AddUserRequest addUser)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            bool isExsitUser = await FreeSql.Select<UserInfo>().Where(o => o.Phone == addUser.Phone).WhereIf(string.IsNullOrWhiteSpace(addUser.UserName), o => o.UserName == addUser.UserName).AnyAsync();
            if (isExsitUser)
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
                    UserName = string.IsNullOrWhiteSpace(addUser.UserName) ? addUser.Phone : addUser.UserName,
                    Password = DefaultPassword.PASSWORD.ToMd5(),
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
                    AddUserId = CurrentUserInfo.Id,
                    AddUserRealName = CurrentUserInfo.RealName,
                    UpdateTime = DateTime.Now,
                    UpdateUserId = CurrentUserInfo.Id,
                    UpdateUserRealName = CurrentUserInfo.RealName,
                };
                int count = await FreeSql.Insert<UserInfo>(userInfo).ExecuteAffrowsAsync();
                response.Data = userInfo.Id;
            }

            return await response.ToJsonResultAsync();
        }


    

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="getUsers"></param>
        /// <returns></returns>
        [HttpPost("api/user/getusers")]
        [MoudleInfo("获取用户列表")]
        public async Task<ActionResult<PageResponseBase<IEnumerable<UserInfo>>>> GetUsers(GetUsersRequest getUsers)
        {
            PageResponseBase<IEnumerable<UserInfo>> response = new PageResponseBase<IEnumerable<UserInfo>>();

            var selectUserInfo = FreeSql.Select<UserInfo>()
                   .WhereIf(!string.IsNullOrEmpty(getUsers.Name), o => o.UserName.Contains(getUsers.Name) || o.RealName.Contains(getUsers.Name) == true || o.RealNamePin.ToLower().Contains(getUsers.Name.ToLower()) == true)
                   .WhereIf(getUsers.Status != null, o => o.Status == getUsers.Status)
                   .WhereIf(!string.IsNullOrEmpty(getUsers.Phone), o => o.Phone.Contains(getUsers.Phone))
                   .WhereIf(!string.IsNullOrEmpty(getUsers.Email), o => o.Email.Contains(getUsers.Email))
                   .WhereIf(!string.IsNullOrEmpty(getUsers.IdCard), o => o.IdCard.Contains(getUsers.IdCard))
                   .WhereIf(getUsers.Sex != null, o => o.Sex == getUsers.Sex)
                   .WhereIf(getUsers.BirthDayStart != null, o => o.BirthDay >= getUsers.BirthDayStart)
                   .WhereIf(getUsers.BirthDayEnd != null, o => o.BirthDay <= getUsers.BirthDayEnd);

            response.TotalCount = await selectUserInfo.CountAsync();

            response.Data = await selectUserInfo.OrderBy(getUsers.OrderBys).Page(getUsers.PageIndex, getUsers.PageSize).ToListAsync();


            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="setUserStatus"></param>
        /// <returns></returns>
        [HttpPost("api/user/setuserstatus")]
        [MoudleInfo("设置用户状态")]
        public async Task<ActionResult<ResponseBase>> SetUserStatus(SetUserStatusRequest setUserStatus)
        {
            ResponseBase response = new ResponseBase();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == setUserStatus.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {

                int count = await FreeSql.Update<UserInfo>(setUserStatus.UserId)
                      .Set(a => a.Status == setUserStatus.Status)
                      .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();

            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="resetUserPwd"></param>
        /// <returns></returns>
        [HttpPost("api/user/resetuserpwd")]
        [MoudleInfo("重置用户密码")]
        public async Task<ActionResult<ResponseBase>> ResetUserPwd(ResetUserPwdRequest resetUserPwd)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == resetUserPwd.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Update<UserInfo>(CurrentUserInfo.Id)
                       .Set(a => a.Password == resetUserPwd.NewPassword.ToMd5())
                       .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();
                response.Data = count;
            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 修改用户简介
        /// </summary>
        /// <param name="setUserProfile"></param>
        /// <returns></returns>
        [HttpPost("api/user/setuserprofile")]
        [MoudleInfo("修改用户简介")]
        public async Task<ActionResult<ResponseBase>> SetUserProfile(SetUserProfileRequest setUserProfile)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == setUserProfile.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                string pinyin = setUserProfile.RealName?.ToPinYin();
                int count = await FreeSql.Update<UserInfo>(CurrentUserInfo.Id)
                     .Set(a => a.Phone == setUserProfile.Phone)
                     .Set(a => a.Email == setUserProfile.Email)
                     .Set(a => a.RealName == setUserProfile.RealName)
                     .Set(a => a.RealNamePin == pinyin)
                     .Set(a => a.Qq == setUserProfile.Qq)
                     .Set(a => a.WxNo == setUserProfile.WxNo)
                     .Set(a => a.TelPhone == setUserProfile.TelPhone)
                     .Set(a => a.PositionName == setUserProfile.PositionName)
                     .Set(a => a.Province == setUserProfile.Province)
                     .Set(a => a.City == setUserProfile.City)
                     .Set(a => a.Area == setUserProfile.Area)
                     .Set(a => a.Address == setUserProfile.Address)
                     .Set(a => a.HeadImageUrl == setUserProfile.HeadImageUrl)
                     .Set(a => a.Remark == setUserProfile.Remark)
                     .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();

                response.Data = count;

            }
            return await response.ToJsonResultAsync();

        }



        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="setUserAuth"></param>
        /// <returns></returns>
        [HttpPost("api/user/setuserauth")]
        [MoudleInfo("修改用户授权")]
        public async Task<ActionResult<ResponseBase>> SetUserAuth(SetUserAuthRequest setUserAuth)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == setUserAuth.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Update<UserInfo>(CurrentUserInfo.Id)
                                       .SetIf(setUserAuth.Type != null, a => a.Type == setUserAuth.Type)
                                       .SetIf(setUserAuth.ExpiredTime != null, a => a.ExpiredTime == setUserAuth.ExpiredTime)
                                       .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();

                response.Data = count;

            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("api/user/deleteuser")]
        [MoudleInfo("删除用户")]
        public async Task<ActionResult<ResponseBase>> DeleteUser(string id)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            if (id.IsNullOrWhiteSpace())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "id不能为空";
                return response;
            }
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == id).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Delete<UserInfo>().Where(o => o.Id == id).ExecuteAffrowsAsync();
                response.Data = count;

            }
            return await response.ToJsonResultAsync();

        }






      



    }
}