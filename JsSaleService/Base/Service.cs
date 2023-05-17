using JwSale.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsSaleService
{
    public abstract class Service
    {
        protected JwSaleDbContext DbContext { get; }
        protected IFreeSql FreeSql { get; }
        public Service(JwSaleDbContext dbContext, IFreeSql freeSql)
        {
            DbContext = dbContext;
            FreeSql = freeSql;
        }
    }
}
