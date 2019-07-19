using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Repository.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LogAttribute : ActionFilterAttribute
    {
        JwSaleDbContext jwSaleDbContext;
        public LogAttribute(JwSaleDbContext jwSaleDbContext)
        {
            this.jwSaleDbContext = jwSaleDbContext;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {

        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
           


            SysLog sysLog = new SysLog()
            {
                Id = Guid.NewGuid(),
                Message = "",
                Name = "",
                Type = 1,



                AddUserId = UserHelper.UserInfo.Id,
                AddUserRealName = UserHelper.UserInfo.AddUserRealName,
                UpdateUserId = UserHelper.UserInfo.Id,
                UpdateUserRealName = UserHelper.UserInfo.AddUserRealName,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };






        }
    }
}
