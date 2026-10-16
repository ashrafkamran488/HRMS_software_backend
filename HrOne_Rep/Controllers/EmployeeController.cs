using ClosedXML.Excel;
using HrOne_Rep.DTOs;
using HrOne_Rep.IRepository;
using HrOne_Rep.Models;
using HrOne_Rep.Services;
using HrOne_Rep.Services.HrOne_Rep.Services;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace HrOne_Rep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly PdfService _pdfService;
        private readonly ExcelService _excelService;

        public EmployeeController(IEmployeeRepository employeeRepository, PdfService pdfService, ExcelService excelService)
        {
            _employeeRepository = employeeRepository;
            _pdfService = pdfService;
            _excelService = excelService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var employees =
            await _employeeRepository.GetAllAsync();

            return Ok(employees);
        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new
                {
                    Message = "Employee not found"
                });
            }

            return Ok(employee);
        }


        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeCreateDto employee)
        {
            var result = await _employeeRepository.CreateAsync(employee);


            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto dto)
        {
            var result = await _employeeRepository.UpdateAsync(id, dto);

            if (result == null)
            {
                return NotFound(new
                {
                    Message = "Employee Not Found"
                });
            }

            return Ok(result);
        }



        [HttpGet("ExportEmployeeListPdf")]
        public async Task<IActionResult> ExportEmployeeListPdf()
        {
            var employees =
                await _employeeRepository.GetAllAsync();

            var pdfBytes =
                _pdfService.GenerateEmployeePdf(employees);

            return File(
                pdfBytes,
                "application/pdf",
                "Employees.pdf");
        }


        [HttpGet("ExportEmployeeListExcel")]
        public async Task<IActionResult> ExportEmployeeListExcel()
        {
            var employees =
                await _employeeRepository.GetAllAsync();

            var excelBytes =
                _excelService.GenerateEmployeeExcel(employees);

            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "EmployeeReport.xlsx");
        }


    }
}
