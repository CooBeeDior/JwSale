using Hangfire.Logging;
using JwSale.Packs.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
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
            var provider = services.BuildServiceProvider();
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseJwSale();
        }
    }

}
