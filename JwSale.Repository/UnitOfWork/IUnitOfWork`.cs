using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.UnitOfWork
{
    public interface IUnitOfWork<TDbContext> : IUnitOfWork, IDisposable where TDbContext : DbContext
    {

    }
}
