using JwSale.Repository.Context;
using JwSale.Util.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace JwSale.Repository.UnitOfWork
{
    [Dependecy(ServiceLifetime.Scoped)]
    public class JwSaleUnitOfWork : UnitOfWork<JwSaleDbContext>, IJwSaleUnitOfWork
    {
        public JwSaleUnitOfWork(JwSaleDbContext jwSaleDbContext) : base(jwSaleDbContext)
        {


        }
    }
}
