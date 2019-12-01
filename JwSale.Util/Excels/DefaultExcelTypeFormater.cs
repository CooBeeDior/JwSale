using System;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;

namespace JwSale.Util.Excels
{
    public class DefaultExcelTypeFormater : IExcelTypeFormater
    {
        public virtual Action<ExcelRangeBase, object> SetBodyCell()
        {
            return (c, o) =>
            {
                c.Value = o;
            };
        }

        public virtual Action<ExcelRangeBase, object> SetHeaderCell()
        {
            return (c, o) =>
            {
                c.Value = o;
            };
        }
    }
}
