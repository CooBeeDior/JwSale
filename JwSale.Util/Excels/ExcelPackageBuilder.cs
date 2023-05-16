using JwSale.Util.Attributes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JwSale.Util.Excels
{
    public static class ExcelPackageBuilder
    {

        public static ExcelPackage Build()
        {
            return new ExcelPackage();
        }

        public static ExcelPackage Build(Stream sm)
        {
            return new ExcelPackage(sm);
        }






    }
}
