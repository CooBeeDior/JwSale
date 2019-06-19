using JwSale.Model.Enums;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Repository.Filters;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Attributes;
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

            var configuration = services.GetSingletonInstance<IConfiguration>();

            var jwSaleOptions = configuration.Get<JwSaleOptions>();

            var jwSaleSqlServer = jwSaleOptions?.JwSaleSqlServers?.Where(o => o.DbContextTypeName == typeof(TDbContext).FullName)?.FirstOrDefault();

            if (jwSaleSqlServer?.DatabaseType == DatabaseType.MsSqlServer)
            {
                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlServer(jwSaleSqlServer.ConnectionString);
                    if (jwSaleSqlServer.UseLazyLoadingProxies)
                    {
                        options.UseLazyLoadingProxies();
                    }

                });
            }
            else
            {
                throw new System.Exception($"未找到{typeof(TDbContext).FullName}数据库配置");
            } 

            return services;



        }










    }
}
