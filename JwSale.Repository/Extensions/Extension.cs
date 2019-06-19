using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace JwSale.Repository.Extensions
{
    public static class Extension
    {     /// <summary>
          /// 当前上下文是否是关系型数据库
          /// </summary>
        public static bool IsRelationalTransaction(this DbContext context)
        {
            return context.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager;
        }

        /// <summary>
        /// 检测关系型数据库是否存在
        /// </summary>
        public static bool ExistsRelationalDatabase(this DbContext context)
        {
            RelationalDatabaseCreator creator = context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            return creator != null && creator.Exists();
        }
    }
}
