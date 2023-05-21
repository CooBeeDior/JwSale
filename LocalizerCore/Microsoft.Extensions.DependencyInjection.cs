using FreeSql;
using FreesqlCore;
using LocalizerAbstraction;
using LocalizerCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizerAbstraction
    {

        public static void AddMongodbLocalizer(this IServiceCollection services, Action<MongodbLocalizerOptions> action = null)
        {
            MongodbLocalizerOptions options = new MongodbLocalizerOptions();
            action?.Invoke(options);
            services.AddSingleton(options);
            //本地序列化
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, MongodbStringLocalizerFactory>();

        }

        public static void AddMsSqlLocalizer(this IServiceCollection services, Action<MsSqlStringLocalizerOptions> action = null)
        {
            MsSqlStringLocalizerOptions options = new MsSqlStringLocalizerOptions();
            action?.Invoke(options);
            services.AddSingleton(options);
         

            services.AddFreeSqlWithIdleBus(s =>
            {
                var db0 = new FreeSqlDbOptions()
                {
                    Name = nameof(MsSqlStringLocalizerOptions),
                    DataType = DataType.SqlServer,
                    ConnectString = options.ConnectString,

                };
                s.FreeSqlDbs.Add(db0);
            });
            //本地序列化
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, MsSqlStringLocalizerFactory>();

        }
    }

}
