using System;
using System.Collections.Generic;
using System.Text;
using FreeSql;
using JwSale.Model;

namespace FreeSqlCore
{
    public class FreeSqlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
         
        }

        protected override void OnModelCreating(ICodeFirst codefirst)
        {
   
        }
    }
}
