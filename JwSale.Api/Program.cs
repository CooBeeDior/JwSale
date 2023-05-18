using JwSale.Api.Http;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Service;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace JwSale.Api
{

    public class Program
    {

        public static void Main(string[] args)
        {
            
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                File.AppendAllText("application.error.log", ex.Message);

            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
