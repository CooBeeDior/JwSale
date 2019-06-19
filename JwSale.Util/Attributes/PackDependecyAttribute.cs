using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JwSale.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PackDependecyAttribute : Attribute
    {
        public IList<Type> PackDependecyTypes { get; private set; }


        public PackDependecyAttribute(params Type[] types)
        {
            PackDependecyTypes = new List<Type>();
            foreach (var item in types)
            {
                PackDependecyTypes.Add(item);
            }

        }


    }
}
