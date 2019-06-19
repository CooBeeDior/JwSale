using JwSale.Model;
using JwSale.Util.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JwSale.Repository.Repositorys
{
    public class Repository<TDbContext, TEntity> : IRepository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : class, IEntity
    {
        protected TDbContext dbContext;
        protected DbSet<TEntity> dbSet;



        public Repository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }


        /// <summary>
        /// Gets the data context object.
        /// </summary>
        protected virtual TDbContext DbContext { get { return dbContext; } }

        /// <summary>
        /// Gets the current DbSet object.
        /// </summary>
        protected virtual DbSet<TEntity> DbSet { get { return dbSet; } }

        /// <summary>
        /// Dispose the class.
        /// </summary>
        public void Dispose()
        {
            dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get all objects.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> All()
        {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Gets objects by specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate object.</param>
        /// <returns>return an object collection result.</returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TEntity>();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TEntity Create(TEntity TEntity)
        {
            var newEntry = DbSet.Add(TEntity);

            dbContext.SaveChanges();
            return newEntry.Entity;
        }

        public virtual void Delete(TEntity TEntity)
        {
            var entry = dbContext.Entry(TEntity);
            DbSet.Remove(TEntity);

            dbContext.SaveChanges();
        }

        public virtual TEntity Update(TEntity TEntity)
        {
            var entry = dbContext.Entry(TEntity);
            DbSet.Attach(TEntity);
            entry.State = EntityState.Modified;

            dbContext.SaveChanges();
            return TEntity;
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = DbSet.Where(predicate).ToList();
            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }

            return dbContext.SaveChanges();

        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public int Submit()
        {
            return dbContext.SaveChanges();
        }


    }
}
