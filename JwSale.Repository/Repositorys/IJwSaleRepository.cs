using JwSale.Model;
using JwSale.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.Repositorys
{
    public interface IJwSaleRepository<TEntity> : IRepository<JwSaleDbContext, TEntity> where TEntity : class, IEntity
    {
    
    }
}
