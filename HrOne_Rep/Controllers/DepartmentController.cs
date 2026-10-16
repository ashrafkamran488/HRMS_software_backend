using HrOne_Rep.DTOs.Department;
using HrOne_Rep.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrOne_Rep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(
        IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet("GetAllDepartmentList")]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentRepository.GetAllDepartmentAsync();

            return Ok(departments);
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department =
                await _departmentRepository.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(CreateDepartmentDto department)
        {
            await _departmentRepository.CreateDepartmentAsync(department);
            return Ok(new
            {
                Message = "Department Added Successfully"
            });
        }

        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentUpdateDto dto)
        {
            await _departmentRepository.UpdateAsync(id, dto);

            return Ok(new
            {
                Message = "Department Updated Successfully"
            });
        }

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _departmentRepository.DeleteAsync(id);

        //    return Ok(new
        //    {
        //        Message = "Department Deleted Successfully"
        //    });
        //}

    }
}
