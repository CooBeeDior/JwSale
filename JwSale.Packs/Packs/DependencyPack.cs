using JwSale.Packs.Pack;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Dependencys;
using JwSale.Util.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace JwSale.Packs.Packs
{
    [Pack("依赖注入模块")]
    public class DependencyPack : JwSalePack
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            foreach (var assembly in AssemblyFinder.AllAssembly)
            {
                var dependecyFilterTypes = assembly.GetTypes().Where(o => Attribute.IsDefined(o, typeof(DependecyAttribute)) && o.IsClass && !o.IsAbstract).ToList();

                foreach (var type in dependecyFilterTypes)
                {
                    var attribute = type.GetCustomAttribute<DependecyAttribute>();
                    var inferfaces = type.GetInterfaces()?.Where(o => o != typeof(IDisposable))?.ToList();
                    if (inferfaces == null || inferfaces.Count == 0)
                    {
                        var typeImpl = Activator.CreateInstance(type);
                        services.GetOrAdd(new ServiceDescriptor(type, o =>
                        {
                            return typeImpl;
                        }, attribute.ServiceLifetime));
                    }
                    else
                    {
                        foreach (var item in inferfaces)
                        {                            
                            services.GetOrAdd(new ServiceDescriptor(item, type, attribute.ServiceLifetime));
                        }
                    }
                }
            }
            //services.AddTransient(typeof(Lazy<>));
            ServiceLocator.Instance.SetServiceCollection(services);

            return services;




        }
    }
}
