using JsSaleService;
using JwSale.Model;
using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JwSale.Packs.Packs
{
    [Pack("MoudleInfo模块")]
    public class MoudleInfoPack : JwSalePack
    {
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        protected override void UsePack(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {

                var userService = scope.ServiceProvider.GetService<IUserService>();
                var hospitalService = scope.ServiceProvider.GetService<IHospitalService>();
                userService.InitAdminUserAndRole();
                hospitalService.InitHospital();
                //初始化系统模块信息

                //--SELECT * FROM dbo.FunctionInfo
                //--SELECT* FROM UserInfo
                //--SELECT* FROM RoleInfo
                //--SELECT* FROM UserRoleInfo
                //--SELECT* FROM RolePermissionInfo
                //--SELECT* FROM UserPermissionInfo

                //--DELETE FROM dbo.FunctionInfo
                //--DELETE FROM UserInfo
                //--DELETE FROM RoleInfo
                //--DELETE FROM UserRoleInfo
                //--DELETE FROM RolePermissionInfo
                //--DELETE FROM UserPermissionInfo




            }
        }
    }
}
