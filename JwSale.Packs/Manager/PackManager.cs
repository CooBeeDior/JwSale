﻿using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JwSale.Packs.Manager
{
    public class PackManager : IPackManager
    {


        public IServiceCollection LoadPacks(IServiceCollection services)
        {
            var builder = services.GetSingletonInstance<IJwSalePackBuilder>();
            foreach (var packType in builder.DependencyPacks)
            {
                var pack = Activator.CreateInstance(packType);
                services.GetOrAdd(new ServiceDescriptor(packType, o=> { return pack; }, ServiceLifetime.Singleton));
                var jwSalePack = pack as JwSalePack;
                jwSalePack?.AddBaseServices(services);
            }

            foreach (var packType in builder.Packs)
            {
                var pack = Activator.CreateInstance(packType);
                services.GetOrAdd(new ServiceDescriptor(packType, o => { return pack; }, ServiceLifetime.Singleton));
                var jwSalePack = pack as JwSalePack;
                jwSalePack?.AddBaseServices(services);
            }
            return services;
        }


    }
}
