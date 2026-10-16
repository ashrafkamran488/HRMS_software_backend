using HrOne_Rep.Data;
using HrOne_Rep.DTOs;
using HrOne_Rep.IRepository;
using HrOne_Rep.Models;
using Microsoft.EntityFrameworkCore;

namespace HrOne_Rep.Repository
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeListDto>> GetAllAsync()
        {
            return await _context.Employees.Include(e => e.Department)
                .Select(e => new EmployeeListDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode = e.EmployeeCode,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    MobileNo = e.MobileNo,
                    Salary = e.Salary,
                    JoiningDate = e.JoiningDate,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : string.Empty
                }).ToListAsync();
        }

        public async Task<EmployeeDetailsDto?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDetailsDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode = e.EmployeeCode,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    MobileNo = e.MobileNo,
                    Salary = e.Salary,
                    JoiningDate = e.JoiningDate,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null
                        ? e.Department.DepartmentName
                        : string.Empty
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> CreateAsync(EmployeeCreateDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                MobileNo = dto.MobileNo,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                DepartmentId = dto.DepartmentId
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            employee.EmployeeCode = $"EMP_{employee.EmployeeId:D2}";
            await _context.SaveChangesAsync();



            return employee;
        }

        public async Task<Employee?> UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return null;

            employee.EmployeeCode = dto.EmployeeCode;
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.MobileNo = dto.MobileNo;
            employee.Salary = dto.Salary;
            employee.JoiningDate = dto.JoiningDate;
            employee.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
