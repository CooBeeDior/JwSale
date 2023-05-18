using FreeSql;
using FreesqlCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class FreeSqlExtensions
    {
        public static void AddFreeSqlWithIdleBus(this IServiceCollection services, Action<FreeSqlBusOptions> action = null)
        {
            FreeSqlBusOptions freeSqlOptions = new FreeSqlBusOptions();
            action?.Invoke(freeSqlOptions);
            services.AddSingleton(freeSqlOptions);
            IdleBus<IFreeSql> ib = new IdleBus<IFreeSql>(freeSqlOptions.TimeSpan);
            //Dictionary<string, IFreeSql> dic = new Dictionary<string, IFreeSql>();
            foreach (var item in freeSqlOptions.FreeSqlDbs)
            {
                var isRegisterSucess = ib.TryRegister(item.Name, () =>
                {
                    var freesql = freeSqlBuildService(item);
                    return freesql;
                });
                if (!isRegisterSucess)
                {
                    throw new Exception($"{item.Name}数据库注入失败");
                }

            }
            services.AddSingleton(ib);

        }

        public static void AddFreeSql(this IServiceCollection services, Action<FreeSqlDbOptions> action)
        {
            FreeSqlDbOptions freeSqlOptions = new FreeSqlDbOptions();
            action?.Invoke(freeSqlOptions);
            services.AddSingleton(freeSqlOptions);

            var freesql = freeSqlBuildService(freeSqlOptions);
            services.AddSingleton(freesql);



        }


        private static IFreeSql freeSqlBuildService(FreeSqlDbOptions freeSqlOptions)
        {
            var freesql = new FreeSqlBuilder()
               .UseConnectionString(freeSqlOptions.DataType, freeSqlOptions.ConnectString)
               .UseAutoSyncStructure(freeSqlOptions.IsAutoSyncStructure) //自动同步实体结构到数据库
               .UseNoneCommandParameter(freeSqlOptions.IsNoneCommandParameter)
               .UseLazyLoading(freeSqlOptions.IsLazyLoading)
               .UseNameConvert(freeSqlOptions.NameConvertType)
               .UseMonitorCommand(freeSqlOptions.Executing, freeSqlOptions.Executed).Build();
            freesql.UseJsonMap();

            freesql.Aop.ConfigEntityProperty += (e, obj) =>
            {
                var attr = obj.Property.GetCustomAttribute<TimestampAttribute>();
                if (attr != null)
                {
                    obj.ModifyResult.IsIgnore = true;
                    obj.ModifyResult.IsVersion = true;
                }
            };
            return freesql;
        }
    }
}
