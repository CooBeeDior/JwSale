using JwSale.Api.Filters;
using JwSale.Model;
using JwSale.Repository.Context;
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
    [TypeFilter(typeof(AuthAttribute))]
    [TypeFilter(typeof(LogAttribute))]
    [TypeFilter(typeof(ExceptionAttribute))]
    [ApiController]
    public class JwSaleControllerBase : ControllerBase
    {  


        protected JwSaleDbContext DbContext { get; }
        public JwSaleControllerBase(JwSaleDbContext jwSaleDbContext)
        {
            DbContext = jwSaleDbContext;
        }
    }
}
