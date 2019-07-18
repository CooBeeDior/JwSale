using JwSale.Model.Dto;
using JwSale.Repository.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace JwSale.Api.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthAttribute : ActionFilterAttribute
    {
        JwSaleDbContext jwSaleDbContext;
        public AuthAttribute(JwSaleDbContext jwSaleDbContext)
        {
            this.jwSaleDbContext = jwSaleDbContext;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            

            //ResponseBase response = new ResponseBase();
            //response.Success = false;
            //response.Code = HttpStatusCode.Unauthorized;
            //response.Message = "权限不足";
            //filterContext.Result = new JsonResult(response);
        }
    }
}
