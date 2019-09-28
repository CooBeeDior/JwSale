using JwSale.Packs.Pack;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace JwSale.Packs.Packs
{

    [Pack("MediatR模块")]
    public class MediatRPack : JwSalePack
    {

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            var assemblies = AssemblyFinder.GetAssemblies("JwSale")?.ToArray();
            services.AddMediatR(assemblies);
            return services;
        }



    }
}
