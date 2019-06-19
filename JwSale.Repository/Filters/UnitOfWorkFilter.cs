using JwSale.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Repository.Filters
{
    public class UnitOfWorkFilter : IActionFilter
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        /// <summary>
        /// 初始化一个<see cref="IUnitOfWorkManager"/>类型的新实例
        /// </summary>
        public UnitOfWorkFilter(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
   
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            unitOfWorkManager?.Commit();

        }
    }
}
