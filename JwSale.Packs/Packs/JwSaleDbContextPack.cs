using JwSale.Repository.Context;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace JwSale.Packs.Packs
{
    [Pack("DbContext模块")]
    public class JwSaleDbContextPack : DbContextPack<JwSaleDbContext>
    {
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, JwSaleUnitOfWork>();
            services.GetOrAdd(new ServiceDescriptor(typeof(IJwSaleUnitOfWork), typeof(JwSaleUnitOfWork), ServiceLifetime.Scoped));
            services.GetOrAdd(new ServiceDescriptor(typeof(IJwSaleRepository<>), typeof(JwSaleRepository<>), ServiceLifetime.Scoped));

            return base.AddServices(services);
        }


      
    }
 
}
