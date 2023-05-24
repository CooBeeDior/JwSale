using OfficeCore.Excel.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OfficeCore.Excel.Service
{
    public interface IWorkbookBuilder<TWorkbook>
    {
        TWorkbook CreateWorkbook(ExcelVersion excelVersion= ExcelVersion.Version2007); 


        TWorkbook CreateWorkbook(Stream sm, ExcelVersion excelVersion = ExcelVersion.Version2007); 


        TWorkbook CreateWorkbook(byte[] buffer, ExcelVersion excelVersion = ExcelVersion.Version2007);



        TWorkbook CreateWorkbook(string filename, ExcelVersion excelVersion = ExcelVersion.Version2007);

    }
}
