using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace JwSale.Packs.Packs
{
    [Pack("Mvc模块")]
    public class MvcPack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            return services;
        }


        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        protected override void UsePack(WebApplication app)
        {
            // 2. 默认访问 / 时，返回 index.html
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/index.html");
            });

            app.MapControllers();

        }
    }
}
