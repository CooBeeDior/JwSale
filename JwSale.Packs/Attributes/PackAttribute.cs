using JwSale.Packs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PackAttribute : Attribute
    {
        public string Name { get; }

        public bool IsInitialization { get; set; }

        public Level Level { get; }
        public PackAttribute(string Name, Level Level = Level.Medium, bool IsInitialization = true)
        {
            this.Name = Name;
            this.Level = Level;
            this.IsInitialization = IsInitialization;
        }
    }
}
