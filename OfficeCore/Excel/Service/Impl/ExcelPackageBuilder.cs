using OfficeCore.Excel.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OfficeCore.Excel.Service.Impl
{
    /// <summary>
    /// excel创建者 只支持07之后的excel
    /// </summary>
    public class ExcelPackageBuilder : IWorkbookBuilder<ExcelPackage>
    {
        public ExcelPackage CreateWorkbook(ExcelVersion excelVersion = ExcelVersion.Version2007)
        {
            return new ExcelPackage();
        }


        public ExcelPackage CreateWorkbook(Stream sm, ExcelVersion excelVersion = ExcelVersion.Version2007)
        {
            //
            return new ExcelPackage(sm);
        }

        public ExcelPackage CreateWorkbook(Stream sm, string password, ExcelVersion excelVersion = ExcelVersion.Version2007)
        {
            return new ExcelPackage(sm, password);
        }
        public ExcelPackage CreateWorkbook(byte[] buffer, ExcelVersion excelVersion = ExcelVersion.Version2007)
        {
            return new ExcelPackage(new MemoryStream(buffer));
        }

        public ExcelPackage CreateWorkbook(string path, ExcelVersion excelVersion = ExcelVersion.Version2007)
        {
            return new ExcelPackage(path);
        }

        public ExcelPackage CreateWorkbook(string path, string password, ExcelVersion excelVersion = ExcelVersion.Version2007)
        {
            return new ExcelPackage(path, password);
        }
    }
}
