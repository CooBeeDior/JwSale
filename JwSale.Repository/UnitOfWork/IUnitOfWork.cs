using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JwSale.Repository.UnitOfWork
{  

    public interface IUnitOfWork
    {  /// <summary>
       /// 获取 工作单元的事务是否已提交
       /// </summary>
        bool HasCommitted { get; }


        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        void BeginOrUseTransaction();

        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        /// <param name="cancellationToken">异步取消标记</param>
        /// <returns></returns>
        Task BeginOrUseTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 提交当前上下文的事务更改
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚所有事务
        /// </summary>
        void Rollback();
    }
}
