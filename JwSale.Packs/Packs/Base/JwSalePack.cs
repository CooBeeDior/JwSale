using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Pack
{
    public abstract class JwSalePack
    {

        /// <summary>
        /// 获取 是否已可用
        /// </summary>
        public bool IsEnabled { get; protected set; }



        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public IServiceCollection AddBaseServices(IServiceCollection services)
        {       
            AddServices(services);
            return services;
        }


        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected virtual IServiceCollection AddServices(IServiceCollection services)
        {
            return services;
        }


        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public void UseBasePack(IApplicationBuilder app)
        {
            IsEnabled = true;
            UsePack(app);

        }


        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        protected virtual void UsePack(IApplicationBuilder app)
        {

        }
    }


}
