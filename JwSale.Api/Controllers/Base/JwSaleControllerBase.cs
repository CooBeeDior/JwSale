using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Repository.Context;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Http;
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
        protected JwSaleDbContext DbContext { get; }

  
        public JwSaleControllerBase(JwSaleDbContext jwSaleDbContext)
        {
            DbContext = jwSaleDbContext;
    

        
        }
         

    }
}
