using System;
using System.Collections.Generic;
using System.Text;

namespace FreesqlCore
{
    public static class FreeSqlExtension
    {
        public static IFreeSql GetFreeSql(this IdleBus<IFreeSql> idleBus, FreeSqlBusOptions freeSqlBusOptions)
        {
            string key = freeSqlBusOptions.GetRandomName();
            var freeSql = idleBus.Get(key);
            return freeSql; ;
        }
    }
}
