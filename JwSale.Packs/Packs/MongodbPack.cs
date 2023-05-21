using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace JwSale.Packs.Packs
{
    [Pack("Mongodb模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public class MongodbPack : JwSalePack
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

            //services.AddLogMongodb(options =>
            //{
            //    options = jwSaleOptions.MongoDb;
            //});
            //services.AddLogMongodb(options =>
            //{
            //    options.ConnectionString = "mongodb://coobeedior.com:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
            //    options.DatabaseName = "logdb";
            //    options.CollectionName = "log";
            //});

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
