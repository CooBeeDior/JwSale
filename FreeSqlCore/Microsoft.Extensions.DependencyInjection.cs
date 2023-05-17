using FreeSql;
using FreesqlCore;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                    var freesql = new FreeSqlBuilder()
                     .UseConnectionString(item.DataType, item.ConnectString)
                     .UseAutoSyncStructure(item.IsAutoSyncStructure) //自动同步实体结构到数据库
                     .UseNoneCommandParameter(item.IsNoneCommandParameter)
                     .UseLazyLoading(item.IsLazyLoading)
                     .UseNameConvert(item.NameConvertType)
                     .UseMonitorCommand(item.Executing, item.Executed).Build();
                    freesql.UseJsonMap();
                    return freesql;
                });
                if (!isRegisterSucess)
                {
                    throw new Exception($"{item.Name}数据库注入失败");
                }

            } 
            services.AddSingleton(ib);
         
        }

        public static void AddFreeSql(this IServiceCollection services, Action<FreeSqlDbOptions> action )
        {
            FreeSqlDbOptions freeSqlOptions = new FreeSqlDbOptions();
            action?.Invoke(freeSqlOptions);
            services.AddSingleton(freeSqlOptions);
            try
            {
                var freesql = new FreeSqlBuilder()
                 .UseConnectionString(freeSqlOptions.DataType, freeSqlOptions.ConnectString)
                 .UseAutoSyncStructure(freeSqlOptions.IsAutoSyncStructure) //自动同步实体结构到数据库
                 .UseNoneCommandParameter(freeSqlOptions.IsNoneCommandParameter)
                 .UseLazyLoading(freeSqlOptions.IsLazyLoading)
                 .UseNameConvert(freeSqlOptions.NameConvertType)
                 .UseMonitorCommand(freeSqlOptions.Executing, freeSqlOptions.Executed).Build();
                freesql.UseJsonMap();
                services.AddSingleton(freesql);
            }
            catch (Exception ex)
            { 
            }


        }
    }
}
