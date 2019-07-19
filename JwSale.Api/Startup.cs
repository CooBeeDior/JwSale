using Castle.Core.Logging;
using JwSale.Packs.Manager;
using JwSale.Packs.Options;
using JwSale.Packs.Packs;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Attributes;
using JwSale.Util.Dependencys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace JwSale.Api
{
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
