using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JwSale.Packs.Packs
{
    [Pack("Options模块")]
    public abstract class OptionsBasePack<TOptions> : JwSalePack where TOptions : class, new()
    {
        private Action<BinderOptions> configureBinder = null;

        public OptionsBasePack() : this(null)
        {

        }

        public OptionsBasePack(Action<BinderOptions> configureBinder)
        {
            this.configureBinder = configureBinder;
        }

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            var configuration = services.GetSingletonInstance<IConfiguration>();
            RegisterOptions(services, configuration); 
            return services;
        }


        protected virtual void RegisterOptions(IServiceCollection services, IConfiguration configuration)
        {
            if (configureBinder == null)
            {
                services.Configure<TOptions>(configuration);
            }
            else
            {
                services.Configure<TOptions>(configuration, configureBinder);
            }
        }


    }
}
