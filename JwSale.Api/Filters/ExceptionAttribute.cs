﻿using JwSale.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwSale.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        ILogger<ExceptionAttribute> logger;
        public ExceptionAttribute(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<ExceptionAttribute>();
        }


        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await base.OnExceptionAsync(context);
            ResponseBase response = new ResponseBase();
            response.Success = false;
            response.Code = HttpStatusCode.InternalServerError;
            response.Message = context.Exception.Message;
            logger.LogError(context.Exception, context.Exception.Message);
            context.Result = new JsonResult(response);
        }
    }
}
