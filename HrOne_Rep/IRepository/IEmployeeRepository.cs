using HrOne_Rep.DTOs;
using HrOne_Rep.Models;

namespace HrOne_Rep.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeListDto>> GetAllAsync();
        Task<EmployeeDetailsDto?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(EmployeeCreateDto employee);
        Task<Employee?> UpdateAsync(int id, EmployeeUpdateDto employee);
        Task<bool> DeleteAsync(int id);
    }
}
