﻿using JwSale.Packs.Pack;
using JwSale.Util.Attributes;
using JwSale.Util.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JwSale.Packs.Packs
{
 
    [Pack("Cors模块")]
    public class Log4NetPack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider, Log4NetLoggerProvider>();
            return services;
        }

 
    }
}
