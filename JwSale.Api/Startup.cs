using Hangfire.Logging;
using JwSale.Packs.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace JwSale.Api
{
    /// <summary>
    /// Exceptionless日志对象提供者
 

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
             
            services.AddJwSalePackManager(o => o.AddPackWithPackAttribute<PackAttribute>());
 
   
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UserJwSale();
        }
    }

}
