using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using Microsoft.Extensions.DependencyInjection;
using OfficeCore.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Packs
{
    [Pack("Excel模块")]
    public class ExcelPack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddExcel();
            return services;
        }

 
    }
}
