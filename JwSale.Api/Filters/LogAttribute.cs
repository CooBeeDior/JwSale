using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Packs.Attributes;
using JwSale.Packs.Util;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
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
        ILoggerFactory loggerfactory;
        public LogAttribute(ILoggerFactory loggerfactory)
        {
     
            this.loggerfactory = loggerfactory;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {

            var method = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo;

            var moudleInfoAttribute = method.GetCustomAttributes(false).Where(o => o.GetType() == typeof(MoudleInfoAttribute)).FirstOrDefault() as MoudleInfoAttribute;
            string requestStr = null;
            string responseStr = null;
            if (context.HttpContext.Request.Body.CanSeek && context.HttpContext.Request.Body.CanRead)
            {
                requestStr = Encoding.UTF8.GetString(context.HttpContext.Request.Body.ToBuffer());
            }
            else
            {
                requestStr = (context.HttpContext.Request.QueryString.Value);
            }

            if (context.HttpContext.Response.Body.CanSeek && context.HttpContext.Response.Body.CanRead)
            {
                responseStr = Encoding.UTF8.GetString(context.HttpContext.Response.Body.ToBuffer());
            }


            var logger = loggerfactory.CreateLogger(context.Controller?.GetType() ?? typeof(LogAttribute));
            logger.LogInformation($"{moudleInfoAttribute?.Name}{context.HttpContext.Request.Path} 参数：{requestStr} 返回值:{responseStr}  {context.HttpContext.Connection.RemoteIpAddress.ToString()}");




            //SysLog sysLog = new SysLog()
            //{
            //    Id = Guid.NewGuid(),
            //    Message = $"{requestParams}",
            //    Name = moudleInfoAttribute?.Name ?? context.HttpContext.Request.Path,
            //    Type = moudleInfoAttribute == null ? 0 : moudleInfoAttribute.Type,



            //    AddUserId = DefaultUserInfo.UserInfo.Id,
            //    AddUserRealName = DefaultUserInfo.UserInfo.AddUserRealName,
            //    UpdateUserId = DefaultUserInfo.UserInfo.Id,
            //    UpdateUserRealName = DefaultUserInfo.UserInfo.AddUserRealName,
            //    AddTime = DateTime.Now,
            //    UpdateTime = DateTime.Now,
            //};
            //jwSaleDbContext.Add(sysLog);
            //jwSaleDbContext.SaveChanges();





        }
    }
}
