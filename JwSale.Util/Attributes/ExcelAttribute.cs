using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ExcelAttribute : Attribute
    {
        public string SheetName { get; set; }

        public bool IsIncrease { get; set; }
        /// <summary>
        ///必须继承 IExcelTypeFormater
        /// </summary>
        public Type ExcelType { get; set; }
        public ExcelAttribute(string SheetName = null, bool IsIncrease = false, Type ExcelType = null)
        {
            this.SheetName = SheetName;
            this.IsIncrease = IsIncrease;
            this.ExcelType = ExcelType;
        }
    }
}
