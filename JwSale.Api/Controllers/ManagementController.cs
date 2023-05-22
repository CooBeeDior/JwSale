using JsSaleService;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace JwSale.Api.Controllers
{

    [MoudleInfo("后台管理", 1, IsFunction = false)]
    public class ManagementController : ManageControllerBase
    {
        private readonly IUserService _userService;
        private IDistributedCache _cache;

        private JwSaleOptions jwSaleOptions;
 


        public ManagementController(JwSaleDbContext context, IUserService userService,
            IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions)
        {
            this._userService = userService;
            this._cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;       

        }
    }
}