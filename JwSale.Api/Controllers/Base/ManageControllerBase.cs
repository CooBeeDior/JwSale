using JwSale.Api.Attributes;
using JwSale.Api.Filters;
using JwSale.Model;
using JwSale.Util;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwSale.Api.Controllers
{

    /// <summary>
    /// 管理后台控制器
    /// </summary>
    [TypeFilter(typeof(AuthRequiredFilterAttribute))]
    [TypeFilter(typeof(PermissionRequiredFilterAttribute))]
    [PermissionRequired]
    [AuthRequired]
    public abstract class ManageControllerBase : JwSaleControllerBase
    {
        protected UserInfo CurrentUserInfo { get; private set; }
 
        public ManageControllerBase()
        {
           var httpContextAccessor = ServiceLocator.Instance.GetService<IHttpContextAccessor>();

            CurrentUserInfo = httpContextAccessor.HttpContext.Items[CacheKeyHelper.CURRENTUSER] as UserInfo;

        }
    }
}
