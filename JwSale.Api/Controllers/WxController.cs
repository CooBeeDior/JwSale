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
    /// 微信管理
    /// </summary>
    [MoudleInfo("微信管理", 1)]
    public class WxController : JwSaleControllerBase
    {
        private IDistributedCache cache;

        private JwSaleOptions jwSaleOptions;

        private IHttpContextAccessor accessor;
        public WxController(JwSaleDbContext context, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            this.accessor = accessor;

        }









    }
}
