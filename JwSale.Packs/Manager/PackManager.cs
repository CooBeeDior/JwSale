using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace JwSale.Packs.Manager
{
    public class PackManager : IPackManager
    {


        public IServiceCollection LoadPacks(IServiceCollection services)
        {
            var builder = services.GetSingletonInstance<IJwSalePackBuilder>();
            foreach (var packType in builder.DependencyPacks.OrderBy(o => o.Level))
            {
                var pack = Activator.CreateInstance(packType.Type);
                services.GetOrAdd(new ServiceDescriptor(packType.Type, o => { return pack; }, ServiceLifetime.Singleton));
                var jwSalePack = pack as JwSalePack;
                jwSalePack?.AddBaseServices(services);
            }

            foreach (var packType in builder.Packs.OrderBy(o => o.Level))
            {
                var pack = Activator.CreateInstance(packType.Type);
                services.GetOrAdd(new ServiceDescriptor(packType.Type, o => { return pack; }, ServiceLifetime.Singleton));
                var jwSalePack = pack as JwSalePack;
                jwSalePack?.AddBaseServices(services);
            }
            return services;
        }


    }
}
