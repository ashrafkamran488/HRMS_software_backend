using HrOne_Rep.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace HrOne_Rep.Services
{
    public class PdfService
    {
        public byte[] GenerateEmployeePdf(
            List<EmployeeListDto> employees)
        {
            QuestPDF.Settings.License =
                LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header()
                        .Text("Employee Report")
                        .FontSize(20)
                        .Bold();

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Code");
                                header.Cell().Text("Name");
                                header.Cell().Text("Department");
                                header.Cell().Text("Email");
                            });

                            foreach (var emp in employees)
                            {
                                table.Cell().Text(emp.EmployeeCode);

                                table.Cell().Text(
                                    $"{emp.FirstName} {emp.LastName}");

                                table.Cell().Text(
                                    emp.DepartmentName);

                                table.Cell().Text(
                                    emp.Email);
                            }
                        });
                });
            })
            .GeneratePdf();
        }
    }
}
