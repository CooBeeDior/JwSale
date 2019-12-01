using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Excels
{
    public interface IExcelTypeFormater
    {
        Action<ExcelRangeBase,object> SetHeaderCell(); 
        Action<ExcelRangeBase, object> SetBodyCell();

    }
}
