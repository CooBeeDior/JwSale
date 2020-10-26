using JwSale.Repository.Extensions;
using JwSale.Util.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace JwSale.Repository.UnitOfWork
{

    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private TDbContext dbContext = null;
        private DbTransaction dbTransaction = null;

        private bool _disposed;

        /// <summary>
        /// 初始化一个<see cref="UnitOfWork"/>类型的新实例
        /// </summary>
        public UnitOfWork(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取 事务是否已提交
        /// </summary>
        public bool HasCommitted { get; private set; }




        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        public virtual void BeginOrUseTransaction()
        {
            if (dbTransaction?.Connection == null)
            {
                var dbConnection = dbContext.Database.GetDbConnection();
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }

                dbTransaction = dbConnection.BeginTransaction();

            }

            if (dbContext.Database.CurrentTransaction != null && dbContext.Database.CurrentTransaction.GetDbTransaction() == dbTransaction)
            {
                return;
            }
            if (dbContext.IsRelationalTransaction())
            {
                dbContext.Database.UseTransaction(dbTransaction);

            }
            else
            {
                dbContext.Database.BeginTransaction();
            }
            HasCommitted = false;
        }

        /// <summary>
        /// 对数据库连接开启事务
        /// </summary>
        /// <param name="cancellationToken">异步取消标记</param>
        /// <returns></returns>
        public virtual async Task BeginOrUseTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        { 
            if (dbTransaction?.Connection == null)
            {
                var dbConnection = dbContext.Database.GetDbConnection();
                if (dbConnection.State != ConnectionState.Open)
                {
                 await   dbConnection.OpenAsync();
                } 
                dbTransaction = dbConnection.BeginTransaction (); 
            }
            if (dbContext.Database.CurrentTransaction != null && dbContext.Database.CurrentTransaction.GetDbTransaction() == dbTransaction)
            {
                return;
            }
            if (dbContext.IsRelationalTransaction())
            {
                dbContext.Database.UseTransaction(dbTransaction);

            }
            else
            {
                await dbContext.Database.BeginTransactionAsync(cancellationToken);
            }
            HasCommitted = false;
        }

        /// <summary>
        /// 提交当前上下文的事务更改
        /// </summary>
        public virtual void Commit()
        {
            if (HasCommitted)
            {
                return;
            }
            if (dbTransaction != null)
            {
                dbTransaction.Commit();

                if (dbContext.IsRelationalTransaction())
                {
                    dbContext.Database.CurrentTransaction.Dispose();
                }
                else
                {
                    dbContext.Database.CommitTransaction();
                }

      
            }
            else
            {
                dbContext?.SaveChangesAsync();
            }
            HasCommitted = true;
        }

        /// <summary>
        /// 回滚所有事务
        /// </summary>
        public virtual void Rollback()
        {

            dbTransaction?.Rollback();



            HasCommitted = true;
        }

        private static void CleanChanges(DbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToArray();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        /// <summary>释放对象.</summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            dbTransaction?.Dispose();
            dbContext.Dispose();

            _disposed = true;
        }
    }
}
