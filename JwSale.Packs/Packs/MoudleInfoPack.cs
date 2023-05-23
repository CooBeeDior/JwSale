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

                //初始化系统模块 用户信息
                userService.InitAdminUserAndRole(true);

            }
        }
    }
}
