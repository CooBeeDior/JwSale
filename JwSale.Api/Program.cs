using JwSale.Model.Dto.Request.QrCode;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace JwSale.Api
{
    public class Program
    {
        public static void Main(string[] args)
        { 

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
