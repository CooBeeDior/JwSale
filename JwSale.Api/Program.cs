﻿using Hangfire.Logging;
using JwSale.Api.ConfigureServices;
using JwSale.Packs.Attributes;
using JwSale.Util.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfficeCore.Excel.Test;
using System;
using System.Diagnostics;
using System.IO;

namespace JwSale.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Runtime.Serialization.UseUnsafeBinaryFormatterSerialization", true);

            Console.WriteLine(@"
            _________              ___.                 ________  .__              
            \_   ___ \  ____   ____\_ |__   ____   ____ \______ \ |__| ___________ 
            /    \  \/ /  _ \ /  _ \| __ \_/ __ \_/ __ \ |    |  \|  |/  _ \_  __ \
            \     \___(  <_> |  <_> ) \_\ \  ___/\  ___/ |    `   \  (  <_> )  | \/
             \______  /\____/ \____/|___  /\___  >\___  >_______  /__|\____/|__|   
                    \/                  \/     \/     \/        \/              
");
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLogging(c =>
            {
                c.AddConsole();
                c.AddProvider(new Log4NetLoggerProvider());
            });

            var app = builder.Build();
            app.UseMiddleware<RequestCultureMiddleware>();
            app.UseJwSale();

            Console.WriteLine("服务即将启动..."); 
            app.Run();
        }
    }
}
