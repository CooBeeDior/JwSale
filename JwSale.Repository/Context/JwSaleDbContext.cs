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

        public DbSet<AddressBook> AddressBooks { get; set; }
        public DbSet<ChatRoomInfo> ChatRoomInfos { get; set; }
        public DbSet<ChatRoomMemberInfo> ChatRoomMemberInfos { get; set; }
        public DbSet<ChatRoomTaskInfo> ChatRoomTaskInfos { get; set; }
        public DbSet<WxFriendInfo> WxFriendInfos { get; set; }
        public DbSet<WxInfo> WxInfos { get; set; }
        public DbSet<WxNoTaskInfo> WxNoTaskInfos { get; set; }

        public DbSet<GhInfo> GhInfos { get; set; }
        public DbSet<FunctionInfo> FunctionInfos { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }
        public DbSet<RolePermissionInfo> RolePermissionInfos { get; set; }
        public DbSet<SysDictionary> SysDictionarys { get; set; }
        public DbSet<SysLog> SysLogs { get; set; }

        public DbSet<UserDataPermissionInfo> UserDataPermissionInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserPermissionInfo> UserPermissionInfos { get; set; }
        public DbSet<UserRoleDataPermissionInfo> UserRoleDataPermissionInfos { get; set; }
        public DbSet<UserRoleInfo> UserRoleInfos { get; set; }






    }


}
