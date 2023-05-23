using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Api.Events;
using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Request;
using JwSale.Model.Dto.Response;
using JwSale.Packs.Options;
using JwSale.Util;
using JwSale.Util.Attributes;
using JwSale.Util.Enums;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RabbitmqCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{

    [MoudleInfo("后台管理", 1, IsFunction = false)]
    public class ManagementController : ManageControllerBase
    {
        private readonly IUserService _userService;
        private IDistributedCache _cache;

        private JwSaleOptions jwSaleOptions;



        public ManagementController(IUserService userService,
            IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions)
        {
            this._userService = userService;
            this._cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;

        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="getUsers"></param>
        /// <returns></returns>
        [HttpPost("api/management/queryusers")]
        [MoudleInfo("获取用户列表")]
        public async Task<ActionResult<PageResponseBase<IEnumerable<UserInfo>>>> QueryUsers(QueryUsersRequest getUsers)
        {
            PageResponseBase<IEnumerable<UserInfo>> response = new PageResponseBase<IEnumerable<UserInfo>>();

            var selectUserInfo = FreeSql.Select<UserInfo>()
                   .WhereIf(!string.IsNullOrEmpty(getUsers.Name), o => o.UserName.Contains(getUsers.Name) || o.RealName.Contains(getUsers.Name) == true || o.RealNamePin.ToLower().Contains(getUsers.Name.ToLower()) == true)
                   .WhereIf(getUsers.Status != null, o => o.Status == (int)getUsers.Status)
                   .WhereIf(!string.IsNullOrEmpty(getUsers.Phone), o => o.Phone.Contains(getUsers.Phone))
                   .WhereIf(!string.IsNullOrEmpty(getUsers.Email), o => o.Email.Contains(getUsers.Email))
                   .WhereIf(!string.IsNullOrEmpty(getUsers.IdCard), o => o.IdCard.Contains(getUsers.IdCard))
                   .WhereIf(getUsers.Sex != null, o => o.Sex == (int)getUsers.Sex)
                   .WhereIf(getUsers.BirthDayStart != null, o => o.BirthDay >= getUsers.BirthDayStart)
                   .WhereIf(getUsers.BirthDayEnd != null, o => o.BirthDay <= getUsers.BirthDayEnd);

            response.TotalCount = await selectUserInfo.CountAsync();

            response.Data = await selectUserInfo.OrderBy(getUsers.OrderBys).Page(getUsers.PageIndex, getUsers.PageSize).ToListAsync();


            return await response.ToJsonResultAsync();
        }






    }
}