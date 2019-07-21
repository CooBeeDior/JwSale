using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Response.User;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : JwSaleControllerBase
    {
        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private IHttpContextAccessor accessor;
        public UserController(JwSaleDbContext context, IDistributedCache cache, JwSaleOptions jwSaleOptions, IHttpContextAccessor accessor) 
        {
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions;
            this.accessor = accessor;

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [NoPermissionRequired]
        [HttpPost("api/User/Login")]
        public async Task<HttpResponseMessage> Login(Login login)
        {
            ResponseBase<LoginResponse> response = new ResponseBase<LoginResponse>();

            var userinfo = DbContext.UserInfos.Where(o => o.UserName == login.UserName).FirstOrDefault();
            if (userinfo != null)
            {
                if (string.Compare(userinfo.Password.ToMd5(), userinfo.Password, true) == 0)
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
                        ExpiredTime = userToken.AddTime.AddSeconds(userToken.Expireds)
                    };

                    await cache.SetStringAsync(CacheKeyHelper.GetUserTokenKey(userinfo.UserName), loginResponse.ToJson());
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
            return await response.ToHttpResponseAsync();
        }













        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/User/Logout")]
        public async Task<HttpResponseMessage> Logout()
        {
            ResponseBase response = new ResponseBase();

            LoginResponse loginResponse = new LoginResponse();
            return await response.ToHttpResponseAsync();
        }

    }
}
