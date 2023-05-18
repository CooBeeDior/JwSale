using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 管理后台控制器
    /// </summary>
    [TypeFilter(typeof(AuthRequiredAttribute))]
    [TypeFilter(typeof(PermissionRequiredAttribute))]
    public abstract class ManageControllerBase : JwSaleControllerBase
    {
        protected UserInfo UserInfo { get; private set; }
        public ManageControllerBase(JwSaleDbContext jwSaleDbContext) : base(jwSaleDbContext)
        {
            var accessor = ServiceLocator.Instance.GetService<IHttpContextAccessor>();

            UserInfo = accessor.HttpContext.Items[CacheKeyHelper.WECHATUSER] as UserInfo;
 
        }
    }
}
