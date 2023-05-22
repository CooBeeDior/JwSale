using JwSale.Api.Attributes;
using JwSale.Api.Filters;
using Microsoft.AspNetCore.Mvc;

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
