﻿using Hangfire;
using Hangfire.MySql;
using Hangfire.SqlServer;
using JwSale.Model.Enums;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace JwSale.Packs.Packs
{

    [Pack("HangFire模块", IsInitialization = false)]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    [PackDependecy(typeof(JwSaleDbContextPack))]
    public class HangFirePack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            var configuration = services.GetSingletonInstance<IConfiguration>();
            var jwSaleOptions = configuration.Get<JwSaleOptions>();
            if (jwSaleOptions?.HangFire?.Enabled == true)
            {
                if (jwSaleOptions?.HangFire?.DatabaseType == DatabaseType.MsSqlServer)
                {
                    services.AddHangfire(x => x.UseSqlServerStorage(jwSaleOptions.HangFire.ConnectionString));
                }
                else if (jwSaleOptions?.HangFire?.DatabaseType == DatabaseType.MySql)
                {
                    services.AddHangfire(x => x.UseStorage(new MySqlStorage(jwSaleOptions.HangFire.ConnectionString,
                        new MySqlStorageOptions
                        {
                        
                        })));
                }
                else
                {
                    services.AddHangfire(x => x.UseSqlServerStorage(jwSaleOptions.HangFire.ConnectionString));
                }

            }

            return services;
        }


        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        protected override void UsePack(IApplicationBuilder app)
        {
            JwSaleOptions jwSaleOptions = app.ApplicationServices.GetService<IOptions<JwSaleOptions>>()?.Value;
            if (jwSaleOptions?.HangFire?.Enabled == true)
            {
                app.UseHangfireServer();//启动Hangfire服务
                app.UseHangfireDashboard();//启动hangfire面板
            }
            //Demo
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Minutely());

        }
    }
}
