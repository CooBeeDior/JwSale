using JwSale.Repository.Context;

namespace JwSale.Repository.UnitOfWork
{
    //[Dependecy(ServiceLifetime.Scoped)]
    public class JwSaleUnitOfWork : UnitOfWork<JwSaleDbContext>, IJwSaleUnitOfWork
    {
        public JwSaleUnitOfWork(JwSaleDbContext jwSaleDbContext) : base(jwSaleDbContext)
        {


        }
    }
}
