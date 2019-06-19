using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PackAttribute : Attribute
    {
        public string Name { get; }
        public PackAttribute(string name = null)
        {
            this.Name = name;
        }
    }
}
