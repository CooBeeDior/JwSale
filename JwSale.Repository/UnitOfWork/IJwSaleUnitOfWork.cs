using JwSale.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.UnitOfWork
{
    public interface IJwSaleUnitOfWork : IUnitOfWork<JwSaleDbContext>
    {
    }
}
