using ClosedXML.Excel;
using HrOne_Rep.DTOs;

namespace HrOne_Rep.Services
{
    namespace HrOne_Rep.Services
    {
        public class ExcelService
        {
            public byte[] GenerateEmployeeExcel(
                List<EmployeeListDto> employees)
            {
                using var workbook = new XLWorkbook();

                var worksheet =
                    workbook.Worksheets.Add("Employees");

                // Header
                worksheet.Cell(1, 1).Value = "Employee Code";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Department";
                worksheet.Cell(1, 4).Value = "Email";

                // Header Style
                var headerRange =
                    worksheet.Range("A1:D1");

                headerRange.Style.Font.Bold = true;

                headerRange.Style.Fill
                    .BackgroundColor = XLColor.LightBlue;

                int row = 2;

                foreach (var emp in employees)
                {
                    worksheet.Cell(row, 1).Value =
                        emp.EmployeeCode;

                    worksheet.Cell(row, 2).Value =
                        $"{emp.FirstName} {emp.LastName}";

                    worksheet.Cell(row, 3).Value =
                        emp.DepartmentName;

                    worksheet.Cell(row, 4).Value =
                        emp.Email;

                    row++;
                }

                worksheet.Columns()
                    .AdjustToContents();

                using var stream =
                    new MemoryStream();

                workbook.SaveAs(stream);

                return stream.ToArray();
            }
        }
    }
}
