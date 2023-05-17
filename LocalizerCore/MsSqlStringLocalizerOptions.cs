using FreeSql;
using FreeSql.Internal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LocalizerCore
{
    public class MsSqlStringLocalizerOptions
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }

        public string ConnectString { get; set; }
        /// <summary>
        /// 是否自动同步实体结构到数据库
        /// </summary>
        public bool IsAutoSyncStructure { get; set; }

        /// <summary>
        /// sql是否采用参数化
        /// </summary>
        public bool IsNoneCommandParameter { get; set; }


        public bool IsLazyLoading { get; set; }

        public NameConvertType NameConvertType { get; set; } = NameConvertType.None;

        public Action<DbCommand> Executing { get; set; }

        public Action<DbCommand, string> Executed { get; set; }
    }
}
