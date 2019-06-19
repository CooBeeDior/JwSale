using Microsoft.Extensions.DependencyInjection;

namespace JwSale.Packs.Manager
{
    public interface IPackManager
    {
        /// <summary>
        /// 加载模块服务
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <returns>服务容器</returns>
        IServiceCollection LoadPacks(IServiceCollection services);

     
    }
}
