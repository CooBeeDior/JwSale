using JwSale.Packs.Manager;
using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入服务集合扩展
    /// </summary>
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwSalePackManager(this IServiceCollection services)
        {
            services.CheckNotNull("services");
            IJwSalePackBuilder builder = services.GetSingletonInstanceOrNull<IJwSalePackBuilder>() ?? new JwSalePackBuilder();

            services.TryAddSingleton<IJwSalePackBuilder>(builder);

            PackManager manager = new PackManager();
            services.AddSingleton<IPackManager>(manager);
            manager.LoadPacks(services);
            return services;
        }
        public static IServiceCollection AddJwSalePackManager(this IServiceCollection services, Action<IJwSalePackBuilder> builderAction)
        {
            services.CheckNotNull("services");
            IJwSalePackBuilder builder = services.GetSingletonInstanceOrNull<IJwSalePackBuilder>() ?? new JwSalePackBuilder();
            builderAction?.Invoke(builder);
            services.TryAddSingleton<IJwSalePackBuilder>(builder);

            PackManager manager = new PackManager();
            services.AddSingleton<IPackManager>(manager);
            manager.LoadPacks(services);


            foreach (var pack in builder.Packs)
            {
                services.TryAddSingleton(pack);
            }

            return services;
        }
        /// <summary>
        /// 将OSharp服务，各个<see cref="OsharpPack"/>模块的服务添加到服务容器中
        /// </summary>
        public static IServiceCollection AddJwSalePackManager<TPackManager>(this IServiceCollection services, Action<IJwSalePackBuilder> builderAction = null)
            where TPackManager : IPackManager, new()
        {
            services.CheckNotNull("services");

            IJwSalePackBuilder builder = services.GetSingletonInstanceOrNull<IJwSalePackBuilder>() ?? new JwSalePackBuilder();
            builderAction?.Invoke(builder);
            services.TryAddSingleton<IJwSalePackBuilder>(builder);


            TPackManager manager = new TPackManager();
            services.AddSingleton<IPackManager>(manager);
            manager.LoadPacks(services);
            return services;
        }





        public static void UseJwSale(this IApplicationBuilder app)
        {
            var packBuilder = app.ApplicationServices.GetService<IJwSalePackBuilder>();
            foreach (var packType in packBuilder.Packs.OrderBy(o => o.Level))
            {
                var pack = app.ApplicationServices.GetService(packType.Type) as JwSalePack;
                pack?.UseBasePack(app);
            }
        }


    }
}
