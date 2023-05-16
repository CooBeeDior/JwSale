using JwSale.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JwSql.Con
{
    class Program
    {
        static void Main(string[] args)
        {


            DbContextOptionsBuilder<JwSaleDbContext> builder = new DbContextOptionsBuilder<JwSaleDbContext>();
            builder.UseSqlServer("Data Source=47.111.87.132;Initial Catalog=ChatRoom;User ID=dev;Password=Zhouqazwsx123");

            JwSaleDbContext jwSaleDbContext = new JwSaleDbContext(builder.Options);


            var wxinfos = jwSaleDbContext.WxInfos.ToList();

        }
    }
}
