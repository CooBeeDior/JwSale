using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EventAttribute : Attribute
    {
        public EventAttribute(string name, bool isInitialization)
        {
            Name = name;
            IsInitialization = isInitialization;
        }
        public EventAttribute(string name) : this(name, false)
        {

        }
        public EventAttribute()
        {

        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool IsInitialization { get; set; }
    }
}
