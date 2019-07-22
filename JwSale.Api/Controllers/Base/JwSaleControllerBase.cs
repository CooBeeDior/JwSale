using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Repository.Context;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 基类
    /// </summary> 
    /// 
    [TypeFilter(typeof(PermissionRequiredAttribute))]
    [TypeFilter(typeof(ValidationModelAttribute))]
    [TypeFilter(typeof(ExceptionAttribute))]
    [ApiController]
    public class JwSaleControllerBase : ControllerBase
    {
        protected JwSaleDbContext DbContext { get; }

        protected UserInfo UserInfo { get; private set; }
        public JwSaleControllerBase()
        {
            DbContext = ServiceLocator.Instance.GetService<JwSaleDbContext>();
            var accessor = ServiceLocator.Instance.GetService<IHttpContextAccessor>();
            UserInfo = accessor.HttpContext.Items[CacheKeyHelper.GetHttpContextUserKey()] as UserInfo;
        }

      


    }
}
