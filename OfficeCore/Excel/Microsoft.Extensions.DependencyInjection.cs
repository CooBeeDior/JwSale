using Microsoft.Extensions.DependencyInjection;
using OfficeCore.Excel.Service;
using OfficeCore.Excel.Service.Impl;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeCore.Excel
{
    public static class ExcelExtensions
    {
        public static IServiceCollection AddExcel(this IServiceCollection services)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            services.AddSingleton<IExcelExportService<ExcelPackage>, ExcelExportService>();
            services.AddSingleton<IExcelImportService<ExcelPackage>, ExcelImportService>();

            services.AddSingleton<IExcelProvider<ExcelPackage>, ExcelProvider>();
            services.AddSingleton<IWorkbookBuilder<ExcelPackage>, ExcelPackageBuilder>();

            return services;
        }
    }
}
