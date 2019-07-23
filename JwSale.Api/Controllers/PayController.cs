using JwSale.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 支付
    /// </summary>
    public class PayController : JwSaleControllerBase
    {
        public PayController(JwSaleDbContext jwSaleDbContext):base(jwSaleDbContext)
        {
        }
    }
}
