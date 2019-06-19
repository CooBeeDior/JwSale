using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace JwSale.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
 
            //string file =   Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            //Assembly assembly = Assembly.GetEntryAssembly() ?? GetCallingAssemblyFromStartup();
            //_loggerRepository = LogManager.CreateRepository(assembly, typeof(Hierarchy));

            //if (File.Exists(file))
            //{
            //    XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo(file));
            //    return;
            //}
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args) 
                .UseStartup<Startup>();


    }
}
