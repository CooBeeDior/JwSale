using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Response.User;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [MoudleInfo("用户管理")]
    public class UserController : JwSaleControllerBase
    {
        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private IHttpContextAccessor accessor;
        public UserController(JwSaleDbContext context, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions, IHttpContextAccessor accessor)
        {
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            this.accessor = accessor;

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
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
                    UserToken userToken = new UserToken()
                    {
                        UserId = userinfo.Id,
                        UserName = userinfo.UserName,
                        Ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                        Expireds = 60 * 60 * 24 * 7,
                        AddTime = DateTime.Now
                    };


                    string token = UserHelper.GenerateToken(userToken, jwSaleOptions.TokenKey);

                    LoginResponse loginResponse = new LoginResponse()
                    {
                        Token = token,
                        ExpiredTime = userToken.AddTime.AddSeconds(userToken.Expireds),
                        UserInfo = userinfo
                    };
                    var cacheEntryOptions = new DistributedCacheEntryOptions() { SlidingExpiration =   TimeSpan.FromSeconds(userToken.Expireds) };
                    await cache.SetStringAsync(CacheKeyHelper.GetUserTokenKey(userinfo.UserName), loginResponse.ToJson(), cacheEntryOptions);
                    response.Data = loginResponse;
                }
                else
                {
                    response.Success = false;
                    response.Message = "密码错误";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }




        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/User/AddUser")]
        [MoudleInfo("添加用户")]
        public async Task<ActionResult<ResponseBase<UserInfo>>> AddUser(AddUser addUser)
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
                    Id = Guid.NewGuid(),
                    UserName = addUser.UserName,
                    Password = addUser.Password.ToMd5(),
                    RealName = addUser.RealName,
                    RealNamePin = addUser.RealName?.ToPinYin(),

                    Phone = addUser.Phone,
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
        /// 获取用户列表
        /// </summary>
        /// <param name="getUsers"></param>
        /// <returns></returns>
        [HttpPost("api/User/GetUsers")]
        [MoudleInfo("获取用户列表")]
        public async Task<ActionResult<PageResponseBase<IEnumerable<UserInfo>>>> GetUsers(GetUsers getUsers)
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
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/User/Logout")]
        [MoudleInfo("退出")]
        public async Task<ActionResult> Logout()
        {
            ResponseBase response = new ResponseBase();

            await cache.RemoveAsync(CacheKeyHelper.GetUserTokenKey(UserInfo.UserName));
            return await response.ToJsonResultAsync();
        }

    }
}
