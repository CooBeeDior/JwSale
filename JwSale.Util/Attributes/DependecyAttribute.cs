using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Attributes
{
    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DependecyAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; }

        public DependecyAttribute(ServiceLifetime ServiceLifetime = ServiceLifetime.Singleton)
        {
            this.ServiceLifetime = ServiceLifetime;
        }
    }
}
