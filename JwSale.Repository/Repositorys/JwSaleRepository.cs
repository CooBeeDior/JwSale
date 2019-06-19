using JwSale.Model;
using JwSale.Repository.Context;
using JwSale.Util.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.Repositorys
{

    //[Dependecy(ServiceLifetime.Scoped)]
    public class JwSaleRepository<TEntity> : Repository<JwSaleDbContext, TEntity>, IJwSaleRepository<TEntity> where TEntity : class, IEntity
    {
        public JwSaleRepository(JwSaleDbContext dbContext) : base(dbContext)
        {

        }
    }
}
