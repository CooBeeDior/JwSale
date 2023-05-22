using JwSale.Api.Attributes;
using JwSale.Api.Filters;
using JwSale.Repository.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 微信控制器
    /// </summary> 
    [TypeFilter(typeof(WechatAuthFilterAttribute))]
    [WechatAuthRequired]
    public abstract class WechatControllerBase : JwSaleControllerBase
    {
        public WechatControllerBase()
        {

        }
    }
}
