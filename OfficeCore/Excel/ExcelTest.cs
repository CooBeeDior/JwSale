using Microsoft.Extensions.DependencyInjection;
using OfficeCore.Excel;
using OfficeCore.Excel.Attributes;
using OfficeCore.Excel.Exceptions;
using OfficeCore.Excel.Extensions;
using OfficeCore.Excel.Service;
using OfficeCore.Excel.Service.Impl;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

namespace OfficeCore.Excel.Test
{

    public class ExcelTest
    {
        private readonly IExcelExportService<ExcelPackage> exportService = null;
        private readonly IExcelImportService<ExcelPackage> excelImportService = null;
        private readonly IWorkbookBuilder<ExcelPackage> workbookBuilder;
        public ExcelTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddExcel();
            var provider = services.BuildServiceProvider();
            exportService = provider.GetService<IExcelExportService<ExcelPackage>>();
            excelImportService = provider.GetService<IExcelImportService<ExcelPackage>>();
            workbookBuilder = provider.GetService<IWorkbookBuilder<ExcelPackage>>();
        }



        /// <summary>
        /// 导出
        /// </summary>
        public void Export()
        {
            IList<Student> students = new List<Student>();
            for (int i = 0; i < 100; i++)
            {
                Student student = new Student()
                {
                    Id = i,
                    Name = $"姓名{i}",
                    Sex = 2,
                    Email = $"aaa{i}@123.com",
                    CreateAt = DateTime.Now.AddDays(-1).AddMinutes(i),
                };
                students.Add(student);
            }
            try
            {

                var excelPackage = exportService.Export<Student>(students).AddSheet<Student>().AddSheet<Student>().AddSheet<Student>().AddSheet<Student>();
                excelPackage.SaveAs("student.xlsx");
            }
            catch (Exception ex)
            {

            }

        }



        /// <summary>
        /// 导出
        /// </summary>
        public void ExportHeader()
        {
            var headers = new List<HeaderInfo>()
            {
               new HeaderInfo("姓名",(cell,o)=>
               {
                   cell.Value=o;
                   cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                   cell.Style.Fill.BackgroundColor.SetColor(Color.Red);
               }),
               new HeaderInfo("性别") ,
               new HeaderInfo("性别") ,
               new HeaderInfo("性别") ,
               new HeaderInfo("性别") ,
               new HeaderInfo("头像") ,

            };
            IList<IList<object>> list = new List<IList<object>>();
            for (int i = 0; i < 10; i++)
            {
                IList<object> cellValues = new List<object>();
                cellValues.Add(new
                {
                    Value = $"姓名{i}",

                });

                cellValues.Add(new
                {
                    Value = i % 3,
                    ExportFormater = new SexExcelTypeFormater()
                });
                cellValues.Add(new
                {
                    Value = i % 3,
                    ExportFormater = new SexExcelTypeFormater()
                });
                cellValues.Add(new
                {
                    Value = i % 3,
                    ExportFormater = new SexExcelTypeFormater()
                });

                cellValues.Add(new
                {
                    Value = $"http://www.baidu.com/{i}",
                    aa = new ImageExcelTypeFormater()
                });
                list.Add(cellValues);

            }
            try
            {
                var ep = workbookBuilder.CreateWorkbook().AddSheetHeader("测试Header", headers).AddBody("测试Body", list);
                ep.SaveAs("header.xlsx");
            }
            catch (Exception ex)
            {

            }
        }


        public void ExportFromDatatable()
        {

            IList<Student> students = new List<Student>();
            for (int i = 0; i < 100; i++)
            {
                Student student = new Student()
                {
                    Id = i,
                    Name = $"姓名{i}",
                    Sex = 2,
                    Email = $"aaa{i}@123.com",
                    CreateAt = DateTime.Now.AddDays(-1).AddMinutes(i),
                };
                students.Add(student);
            }
            try
            {

                DataTable tblDatas = new DataTable("Datas");
                DataColumn dc = null;
                dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
                dc.AutoIncrement = true;//自动增加
                dc.AutoIncrementSeed = 1;//起始为1
                dc.AutoIncrementStep = 1;//步长为1
                dc.AllowDBNull = false;//

                dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));

                DataRow newRow;
                newRow = tblDatas.NewRow();
                newRow["Product"] = "大话西游";
                newRow["Version"] = "2.0";
                newRow["Description"] = "我很喜欢";
                tblDatas.Rows.Add(newRow);

                newRow = tblDatas.NewRow();
                newRow["Product"] = "梦幻西游";
                newRow["Version"] = "3.0";
                newRow["Description"] = "比大话更幼稚";
                tblDatas.Rows.Add(newRow);
                var excelPackage = workbookBuilder.CreateWorkbook().AddSheet(tblDatas);
                excelPackage.SaveAs("datatable.excel");
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 导入
        /// </summary>
        public void Import()
        {
            ExcelPackage ep = null;

            try
            {
                ep = workbookBuilder.CreateWorkbook("student.errors.xlsx");
                var result = excelImportService.Import<Student>(ep);

            }
            catch (ExportExcelException ex)
            {
                ep.AddErrors<Student>(ex.ExportExcelErrors);
                ep.SaveAs("student.custome.errors.xlsx");
            }
            catch (Exception ex) { }

        }

        /// <summary>
        /// 导入错误
        /// </summary>
        public void AddError()
        {
            try
            {
                var ep = workbookBuilder.CreateWorkbook("student.xlsx");
                IList<ExportExcelError> errors = new List<ExportExcelError>();
                ExportExcelError a = new ExportExcelError(2, 3, "自定义错误1");
                ExportExcelError b = new ExportExcelError(3, 3, "自定义错误2");
                errors.Add(a);
                errors.Add(b);

                ep.AddErrors<Student>(errors);
                ep.SaveAs("student.custome.errors.xlsx");
            }
            catch (Exception ex)
            {
            }

        }


    }


    [Excel("学生信息", true, typeof(StudentExcelTypeFormater))]
    public class Student
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ExcelColumn("排序", 1)]
        [Guid()]
        public int Id { get; set; }

        [ExcelColumn("姓名")]
        public string Name { get; set; }


        [ExcelColumn("性别", 3, typeof(SexExcelTypeFormater), typeof(SexExcelImportFormater))]
        public int Sex { get; set; }


        [ExcelColumn("邮箱", 4)]
        [EmailAddress(ErrorMessage = "不是邮件地址格式")]
        public string Email { get; set; }

        [ExcelColumn("创建时间", 5)]
        [IngoreExcelColumn]
        public DateTime CreateAt { get; set; }
    }





    public class StudentExcelTypeFormater : DefaultExcelTypeFormater
    {
        public override Action<ExcelWorksheet> SetExcelWorksheet()
        {
            return (s) =>
            {
                base.SetExcelWorksheet()(s);

                var address = typeof(Student).GetCellAddress(nameof(Student.Email));
                address = $"{address}2:{address}1000";
                var val2 = s.DataValidations.AddCustomValidation(address);
                val2.ShowErrorMessage = true;
                val2.ShowInputMessage = true;
                val2.PromptTitle = "自定义错误信息PromptTitle";
                val2.Prompt = "自定义错误Prompt";
                val2.ErrorTitle = "请输入邮箱ErrorTitle";
                val2.Error = "请输入邮箱Error";
                val2.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                var formula = val2.Formula;
                formula.ExcelFormula = $"=COUNTIF({address},\"?*@*.*\")";
            };

        }

    }


    public class SexExcelTypeFormater : DefaultExcelExportFormater
    {
        public override Action<ExcelRangeBase, object> SetBodyCell()
        {
            return (c, o) =>
            {
                base.SetBodyCell().Invoke(c, o);
                if (int.TryParse(o.ToString(), out int intValue))
                {
                    if (intValue == 1)
                    {
                        c.Value = "男";
                    }
                    else if (intValue == 2)
                    {
                        c.Value = "女";
                    }
                    else
                    {
                        c.Value = "未知";
                    }

                }
                else
                {
                    c.Value = "未知";
                }

            };
        }


    }

    public class SexExcelImportFormater : IExcelImportFormater
    {
        public object Transformation(object origin)
        {
            if (origin == null)
            {
                return 0;
            }
            else if (origin?.ToString() == "男")
            {
                return 1;
            }
            else if (origin?.ToString() == "女")
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }


    public class ImageExcelTypeFormater : DefaultExcelExportFormater
    {
        public override Action<ExcelRangeBase, object> SetBodyCell()
        {
            return (c, o) =>
            {
                c.Style.Font.Size = 12;
                c.Style.Font.UnderLine = true;
                c.Style.Font.Color.SetColor(Color.Blue);
                c.Hyperlink = new Uri(o.ToString(), UriKind.Absolute);
                c.Value = o;


                var fs = File.OpenRead(@"a.jpg");
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                fs.Dispose();
                c.Worksheet.AddPicture(buffer, c, true);


            };
        }


    }

    //
    // 摘要:
    //     Validates an email address.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class GuidAttribute : ValidationAttribute
    {
        //
        // 摘要:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.EmailAddressAttribute
        //     class.
        public GuidAttribute()
        {
            ErrorMessage = "不是Guid的格式";
        }
 
        //
        // 摘要:
        //     Determines whether the specified value matches the pattern of a valid email address.
        //
        // 参数:
        //   value:
        //     The value to validate.
        //
        // 返回结果:
        //     true if the specified value is valid or null; otherwise, false.
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            bool isGuid = Guid.TryParse(value.ToString(), out Guid result);
            if (isGuid)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
