//using JwSale.Model.Enums;
//using JwSale.Packs.Attributes;
//using JwSale.Packs.Enums;
//using JwSale.Packs.Options;
//using JwSale.Repository.Context;
//using JwSale.Repository.Repositorys;
//using JwSale.Repository.UnitOfWork;
//using JwSale.Util.Extensions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System.Linq;

//namespace JwSale.Packs.Packs
//{
//    [Pack("DbContext模块", Level.High)]
//    public class JwSaleDbContextPack : DbContextPack<JwSaleDbContext>
//    {
  
//        protected override IServiceCollection AddServices(IServiceCollection services)
//        {
     
//            services.AddScoped<IUnitOfWork, JwSaleUnitOfWork>();
//            services.GetOrAdd(new ServiceDescriptor(typeof(IJwSaleUnitOfWork), typeof(JwSaleUnitOfWork), ServiceLifetime.Scoped));
//            services.GetOrAdd(new ServiceDescriptor(typeof(IJwSaleRepository<>), typeof(JwSaleRepository<>), ServiceLifetime.Scoped));


//            var configuration = services.GetSingletonInstance<IConfiguration>();

//            var jwSaleOptions = configuration.Get<JwSaleOptions>();

//            var jwSaleSqlServer = jwSaleOptions?.JwSaleSqlServers?.Where(o => o.DbContextTypeName == typeof(JwSaleDbContext).FullName)?.FirstOrDefault();

//            if (jwSaleSqlServer?.DatabaseType == DatabaseType.MsSqlServer)
//            {
//                services.AddDbContext<JwSaleDbContext>(options =>
//                {
//                    options.UseSqlServer(jwSaleSqlServer.ConnectionString);
//                    if (jwSaleSqlServer.UseLazyLoadingProxies)
//                    {
//                        options.UseLazyLoadingProxies();
//                    }

//                });
//            }
//            else
//            {
//                throw new System.Exception($"未找到{typeof(JwSaleDbContext).FullName}数据库配置");
//            }
//            return base.AddServices(services);
//        }


      
//    }
 
//}
