using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace JwSale.Util.Dependencys
{
    public sealed class ServiceLocator : IDisposable
    {
        private static readonly Lazy<ServiceLocator> InstanceLazy = new Lazy<ServiceLocator>(() => new ServiceLocator());


        private IServiceCollection serviceCollection = null;


        private IServiceProvider serviceProvider { get { return initServiceProvider(); }   }

        private bool isInitServiceProvider = false;

        /// <summary>
        /// 初始化一个<see cref="ServiceLocator"/>类型的新实例
        /// </summary>
        private ServiceLocator()
        { }

        private IServiceProvider initServiceProvider()
        {
            IServiceProvider provider = null;
            if (!isInitServiceProvider)
            {
                isInitServiceProvider = true;
                provider = serviceCollection.BuildServiceProvider();
            }
            return provider;
        }

        /// <summary>
        /// 获取 服务器定位器实例
        /// </summary>
        public static ServiceLocator Instance => InstanceLazy.Value;


        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return serviceProvider.GetServices<T>();
        }

        public IServiceCollection SetServiceCollection(IServiceCollection services)
        {
            serviceCollection = services;
            isInitServiceProvider = false;
            return services;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            serviceCollection = null;
 

        }
    }
}
