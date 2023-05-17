using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

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
