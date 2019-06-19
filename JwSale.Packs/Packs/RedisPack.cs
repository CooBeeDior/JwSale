using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwSale.Packs.Packs
{


    [Pack("HangFire模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public class RedisPack : JwSalePack
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
            if (jwSaleOptions.Redis?.Enabled == true)
            {
                services.AddDistributedRedisCache(opts =>
                {
                    opts.Configuration = jwSaleOptions.Redis?.Configuration;
                    opts.InstanceName = jwSaleOptions.Redis?.InstanceName;

                });
            }

            return services;
        }


        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        protected override void UsePack(IApplicationBuilder app)
        {
          
        }
    }
}
