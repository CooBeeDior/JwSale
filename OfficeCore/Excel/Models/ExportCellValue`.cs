using OfficeCore.Excel.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeCore.Excel.Models
{
    public class ExportCellValue<TExcelRange>
    {
        public object Value { get; set; }

        public IExcelExportFormater<TExcelRange> ExportFormater { get; set; }
    }
}
