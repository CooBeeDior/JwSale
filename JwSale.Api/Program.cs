﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OfficeCore.Excel.Test;
using System;
using System.IO;

namespace JwSale.Api
{

    public class Program
    {

        public static void Main(string[] args)
        {      
            Console.WriteLine(@"
            _________              ___.                 ________  .__              
            \_   ___ \  ____   ____\_ |__   ____   ____ \______ \ |__| ___________ 
            /    \  \/ /  _ \ /  _ \| __ \_/ __ \_/ __ \ |    |  \|  |/  _ \_  __ \
            \     \___(  <_> |  <_> ) \_\ \  ___/\  ___/ |    `   \  (  <_> )  | \/
             \______  /\____/ \____/|___  /\___  >\___  >_______  /__|\____/|__|   
                    \/                  \/     \/     \/        \/              

");
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                File.AppendAllText("application.error.log", ex.Message);
                File.AppendAllText("application.error.log", ex.StackTrace);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
