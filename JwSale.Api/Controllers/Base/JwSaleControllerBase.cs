using JwSale.Api.Filters;
using JwSale.Model;
using JwSale.Repository.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 基类
    /// </summary> 
    /// 
    [TypeFilter(typeof(AuthAttribute))]
    [TypeFilter(typeof(ExceptionAttribute))]
    public class JwSaleControllerBase : ControllerBase
    {

        protected UserInfo UserInfo
        {
            get
            {
                return new UserInfo()
                {
                    Id = Guid.Empty,
                    UserName = "admin",
                    AddUserRealName = "超级管理员",

                };
            }
        }




        protected JwSaleDbContext DbContext { get; }
        public JwSaleControllerBase(JwSaleDbContext jwSaleDbContext)
        {
            DbContext = jwSaleDbContext;
        }
    }
}
