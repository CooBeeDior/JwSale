using System;
using System.Collections.Generic;
using System.Text;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.Extensions.DependencyInjection;
namespace JwSale.Repository.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private IEnumerable<IUnitOfWork> unitOfWorks = null;


        public UnitOfWorkManager(IEnumerable<IUnitOfWork> unitOfWorks)
        {
            this.unitOfWorks = unitOfWorks;
        }

        /// <summary>
        /// 获取 事务是否已提交
        /// </summary>
        public bool HasCommitted { get; private set; }

        /// <summary>
        /// 提交所有工作单元的事务更改
        /// </summary>
        public void Commit()
        {
            if (HasCommitted)
            {
                return;
            } 
 
            foreach (var unitOfWork in unitOfWorks)
            {
                unitOfWork?.Commit();
            }

            HasCommitted = true;

        }
    }
}
