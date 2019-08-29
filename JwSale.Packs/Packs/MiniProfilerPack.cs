using JwSale.Packs.Pack;
using JwSale.Util.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;

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
            services.AddMiniProfiler(options =>
            {
                // 设定弹出窗口的位置是左下角
                options.PopupRenderPosition = RenderPosition.BottomLeft;
                // 设定在弹出的明细窗口里会显式Time With Children这列
                options.PopupShowTimeWithChildren = true;
                // 设定访问分析结果URL的路由基地址
                options.RouteBasePath = "/profiler";

            }).AddEntityFramework();
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
