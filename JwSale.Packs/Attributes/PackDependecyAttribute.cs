using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
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
