using JwSale.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.Context
{
    public class JwSaleDbContext : DbContext
    {
        public JwSaleDbContext(DbContextOptions<JwSaleDbContext> options) : base(options)
        {
        }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }

        public DbSet<RoleInfo> RoleInfos { get; set; }
    }

    public class JwSaleDbContext1 : DbContext
    {
        public JwSaleDbContext1(DbContextOptions<JwSaleDbContext1> options) : base(options)
        {
        }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }

        public DbSet<RoleInfo> RoleInfos { get; set; }
    }
}
