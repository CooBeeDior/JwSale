using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MoudleInfoAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 类型 1增 2删 4：改 8：查  16：导入 32：导出
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Order">排序</param>
        /// <param name="Type">类型 1增 2删 4：改 8：查  16：导入 32：导出</param>
        public MoudleInfoAttribute(string Name, int Order = 0, int Type = 8)
        {
            this.Name = Name;
            this.Order = Order;
            this.Type = Type;
        }
    }
}
