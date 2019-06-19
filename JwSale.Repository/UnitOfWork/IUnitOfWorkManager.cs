using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.UnitOfWork
{
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取 事务是否已提交
        /// </summary>
        bool HasCommitted { get; }

        /// <summary>
        /// 提交所有工作单元的事务更改
        /// </summary>
        void Commit();
    }
}
