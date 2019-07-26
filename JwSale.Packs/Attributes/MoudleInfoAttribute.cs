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
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否功能
        /// </summary>
        public bool IsFunction { get; set; }

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
        /// <param name="Code">编码</param>
        /// <param name="IsFunction">是否功能</param>
        /// <param name="Order">排序</param>
        /// <param name="Type">类型 1增 2删 4：改 8：查  16：导入 32：导出</param>
        public MoudleInfoAttribute(string Name, string Code, bool IsFunction, int Order = 0, int Type = 8)
        {
            this.Name = Name;
            this.Code = Code;
            this.IsFunction = IsFunction;
            this.Order = Order;
            this.Type = Type;
        }




        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Name">名称</param> 
        /// <param name="Order">排序</param>
        /// <param name="Type">类型 1增 2删 4：改 8：查  16：导入 32：导出</param>
        public MoudleInfoAttribute(string Name, bool IsFunction = true, int Order = 0, int Type = 8) : this(Name, null, IsFunction, Order, Type)
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Order">排序</param>
        public MoudleInfoAttribute(string Name, int Order) : this(Name, null, true, Order, 8)
        {

        }

    }
}
