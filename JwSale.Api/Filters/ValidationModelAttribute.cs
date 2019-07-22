using JwSale.Model.Dto;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwSale.Api.Filters
{
  
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidationModelAttribute : ActionFilterAttribute
    {
      
      
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Select(x => x.Errors);
                ResponseBase response = new ResponseBase();
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = errors.ToJson();
                context.Result = new JsonResult(response);
            }
        }
    }
}
