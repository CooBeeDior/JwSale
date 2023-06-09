using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeCore.Excel.Service
{
    /// <summary>
    /// 导入格式化
    /// </summary>
    public interface IExcelImportFormater
    {
        object Transformation(object origin);
    }
}
