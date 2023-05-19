using JwSale.Repository.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Filters
{
    public class UnitOfWorkFilterAttribute : TypeFilterAttribute
    {
        public UnitOfWorkFilterAttribute() : base(typeof(UnitOfWorkFilter))
        {

        }
    }
}
