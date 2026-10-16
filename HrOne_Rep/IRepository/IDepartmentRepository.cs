using HrOne_Rep.DTOs;
using HrOne_Rep.DTOs.Department;
using HrOne_Rep.Models;

namespace HrOne_Rep.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDto>> GetAllDepartmentAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<Department> CreateDepartmentAsync(CreateDepartmentDto department);
        Task UpdateAsync(int id, DepartmentUpdateDto department);

        //ask DeleteAsync(int id);
    }
}
