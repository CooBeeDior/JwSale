using JwSale.Packs.Pack;
using JwSale.Util.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace JwSale.Packs.Packs
{

    [Pack("Mvc模块")]
    public class MiniProfilerPack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();
            return services;
        }


        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        protected override void UsePack(IApplicationBuilder app)
        {
            app.UseMiniProfiler();
        }
    }
}
