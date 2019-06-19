using JwSale.Repository.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Filters
{
    public class UnitOfWorkAttribute : TypeFilterAttribute
    {
        public UnitOfWorkAttribute() : base(typeof(UnitOfWorkFilter))
        {

        }
    }
}
