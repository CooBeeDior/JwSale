using JwSale.Model.Enums;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
namespace JwSale.Packs.Packs
{
    [Pack("DbContext模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public abstract class DbContextPack<TDbContext> : JwSalePack where TDbContext : DbContext
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            //services.AddSingleton<UnitOfWorkFilter>();

            services.GetOrAdd(new ServiceDescriptor(typeof(IRepository<,>), typeof(Repository<,>), ServiceLifetime.Scoped));
            services.GetOrAdd(new ServiceDescriptor(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), ServiceLifetime.Scoped));
            services.GetOrAdd(new ServiceDescriptor(typeof(IUnitOfWorkManager), typeof(UnitOfWorkManager), ServiceLifetime.Scoped));

            return services;



        }










    }
}
