using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        public override void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {

            var method = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo;

            var descriptionAttribute = method.GetCustomAttributes(false).Where(o => o.GetType() == typeof(DescriptionAttribute)).FirstOrDefault() as DescriptionAttribute;
            string requestParams;
            if (context.HttpContext.Request.Body.CanSeek && context.HttpContext.Request.Body.CanRead)
            {
                requestParams = Encoding.UTF8.GetString(context.HttpContext.Request.Body.ToBuffer());
            }
            else
            {
                requestParams = (context.HttpContext.Request.QueryString.Value);
            }


            SysLog sysLog = new SysLog()
            {
                Id = Guid.NewGuid(),
                Message = $"{requestParams}\r\n{context.Result.ToJson()}",
                Name = descriptionAttribute?.Description ?? context.HttpContext.Request.Path,
                Type = 1,



                AddUserId = UserHelper.UserInfo.Id,
                AddUserRealName = UserHelper.UserInfo.AddUserRealName,
                UpdateUserId = UserHelper.UserInfo.Id,
                UpdateUserRealName = UserHelper.UserInfo.AddUserRealName,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            jwSaleDbContext.Add(sysLog);
            jwSaleDbContext.SaveChanges();





        }
    }
}
