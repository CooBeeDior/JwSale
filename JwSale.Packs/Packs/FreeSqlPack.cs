using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace JwSale.Packs.Packs
{
    [Pack("FreeSql模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public class FreeSqlPack : JwSalePack
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
            if (jwSaleOptions.FreeSql != null)
            {
                services.AddFreeSql(options =>
                {

                    options.DataType = jwSaleOptions.FreeSql.DataType;
                    options.ConnectString = jwSaleOptions.FreeSql.ConnectString;
                    options.Name = jwSaleOptions.FreeSql.Name;
                    options.IsAutoSyncStructure = jwSaleOptions.FreeSql.IsAutoSyncStructure;
                    options.IsNoneCommandParameter = jwSaleOptions.FreeSql.IsNoneCommandParameter;
                    options.IsLazyLoading = jwSaleOptions.FreeSql.IsLazyLoading;
                    options.NameConvertType = jwSaleOptions.FreeSql.NameConvertType;
                    options.ConnectString = jwSaleOptions.FreeSql.ConnectString;
                    options.Executing = (command) => { };
                    options.Executed = (command, sql) => { };


                });
            }

            if (jwSaleOptions.FreeSqlBus != null && jwSaleOptions.FreeSqlBus.FreeSqlDbs.Count > 0)
            {


                services.AddFreeSqlWithIdleBus(options =>
                {
                    foreach (var item in jwSaleOptions.FreeSqlBus.FreeSqlDbs)
                    {
                        item.Executing = (command) => { };
                        item.Executed = (command, sql) => { };
                        options.FreeSqlDbs.Add(item);
                    }
                });

            }


          

            //services.AddFreeSqlWithIdleBus(options =>
            //{
            //    var db0 = new FreeSqlDbOptions()
            //    {
            //        Name = "Family0",
            //        DataType = FreeSql.DataType.SqlServer,
            //        ConnectString = "Data Source=47.111.87.132;Initial Catalog=Family;User ID=dev;Password=Zhouqazwsx123;Encrypt=True;TrustServerCertificate=True;",
            //        IsAutoSyncStructure = true,
            //        IsNoneCommandParameter = false,
            //        IsLazyLoading = false,
            //        NameConvertType = NameConvertType.PascalCaseToUnderscoreWithLower,
            //        Executing = (command) => { },
            //        Executed = (command, sql) => { }
            //    };
            //    var db1 = new FreeSqlDbOptions()
            //    {
            //        Name = "Family1",
            //        DataType = FreeSql.DataType.SqlServer,
            //        ConnectString = "Data Source=47.111.87.132;Initial Catalog=Family;User ID=dev;Password=Zhouqazwsx123;Encrypt=True;TrustServerCertificate=True;",
            //        IsAutoSyncStructure = true,
            //        IsNoneCommandParameter = false,
            //        IsLazyLoading = false,
            //        NameConvertType = NameConvertType.PascalCaseToUnderscoreWithLower,
            //        Executing = (command) => { },
            //        Executed = (command, sql) => { }
            //    };
            //    options.FreeSqlDbs.Add(db0);
            //    options.FreeSqlDbs.Add(db1);
            //});


            //services.AddFreeSql(options =>
            //{

            //    options.Name = Constant.Family;
            //    options.DataType = FreeSql.DataType.SqlServer;
            //    options.ConnectString = "Data Source=47.111.87.132;Initial Catalog=Family;User ID=dev;Password=Zhouqazwsx123;Encrypt=True;TrustServerCertificate=True;";
            //    options.IsAutoSyncStructure = true;
            //    options.IsNoneCommandParameter = false;
            //    options.IsLazyLoading = false;
            //    options.NameConvertType = NameConvertType.PascalCaseToUnderscoreWithLower;
            //    options.Executing = (command) => { };
            //    options.Executed = (command, sql) => { };
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
