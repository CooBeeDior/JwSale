using JsSaleService;
using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace JwSale.Packs.Packs
{

    [Pack("JsSaleService模块")]
    public class JsSaleServicePack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(IService));
            var types = assembly.GetTypes().Where(o => typeof(IService).IsAssignableFrom(o) && o.IsClass && o.IsPublic && !o.IsAbstract).ToList();
            foreach (var item in types)
            {
                var interfaceType = item.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, item);
                }
                else
                {
                    services.AddScoped(item);
                }

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
