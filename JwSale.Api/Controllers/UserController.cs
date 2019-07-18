using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Response.User;
using JwSale.Repository.Context;
using JwSale.Repository.Filters;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : JwSaleControllerBase
    {
        public UserController(JwSaleDbContext context) : base(context)
        {

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("api/User/Login")]
        public async Task<HttpResponseMessage> Login(Login login)
        {
            ResponseBase<LoginResponse> response = new ResponseBase<LoginResponse>();

            LoginResponse loginResponse = new LoginResponse();
            response.Data = loginResponse;
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
