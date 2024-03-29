﻿using JwSale.Model;
using Microsoft.EntityFrameworkCore;

namespace JwSale.Repository.Context
{
    public class JwSaleDbContext : DbContext
    {
        public JwSaleDbContext(DbContextOptions<JwSaleDbContext> options) : base(options)
        {
        }

        public DbSet<FunctionInfo> FunctionInfos { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }
        public DbSet<RolePermissionInfo> RolePermissionInfos { get; set; }
        public DbSet<SysDictionary> SysDictionarys { get; set; }
        public DbSet<SysLog> SysLogs { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; } 

        public DbSet<UserRoleInfo> UserRoleInfos { get; set; }






    }


}
