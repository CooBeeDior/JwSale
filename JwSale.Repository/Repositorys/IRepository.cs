using JwSale.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JwSale.Repository.Repositorys
{
    /// <summary>
    /// 定义通用的Repository接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<TDbContext, TEntity> : IDisposable where TDbContext : DbContext where TEntity : class, IEntity
    {
        /// <summary>
        /// 获取所有的实体对象
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> All();

        /// <summary>
        /// 通过Lamda表达式过滤符合条件的实体对象
        /// </summary>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        bool Contains(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取实体总数
        /// </summary>
        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过键值查找并返回单个实体
        /// </summary>
        TEntity Find(params object[] keys);

        /// <summary>
        /// 通过表达式查找复合条件的单个实体
        /// </summary>
        /// <param name="predicate"></param>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 创建实体对象
        /// </summary>
        TEntity Create(TEntity entity);

        /// <summary>
        /// 创建实体对象
        /// </summary>
        int BatchCreate(IEnumerable<TEntity> entitys);

        /// <summary>
        /// 删除实体对象
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除符合条件的多个实体对象
        /// </summary>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        TEntity Update(TEntity entity);



        /// <summary>
        /// Save all changes.
        /// </summary>
        /// <returns></returns>
        int Submit();
    }
}
