using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeCore.Excel.Service
{
    /// <summary>
    /// excel格式化
    /// </summary>
    public interface IExcelTypeFormater<TWorksheet>
    {
        Action<TWorksheet> SetExcelWorksheet();

    }
}
