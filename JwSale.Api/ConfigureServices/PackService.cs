using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwSale.Api.Http;
using Microsoft.Extensions.DependencyInjection;
using JwSale.Packs.Attributes;

namespace JwSale.Api.ConfigureServices
{
    public class PackService : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context,services) =>
            {
                services.AddJwSalePackManager(o => o.AddPackWithPackAttribute<PackAttribute>());

            });
        }
    }
}
