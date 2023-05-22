using JwSale.Api.Filters;
using JwSale.Util.Dependencys;
using log4net.Core;
using Microsoft.AspNetCore.Mvc;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 基类
    /// </summary> 
    [TypeFilter(typeof(ValidationModelFilterAttribute))]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    [TypeFilter(typeof(LogFilterAttribute))]
    [ApiController]
    public abstract class JwSaleControllerBase : ControllerBase
    {
        protected ILogger Logger { get; }
        protected IFreeSql FreeSql { get; }
        public JwSaleControllerBase()
        {
            Logger = ServiceLocator.Instance.GetService<ILogger>();
            FreeSql = ServiceLocator.Instance.GetService<IFreeSql>();
        }


    }
}
