using HrOne_Rep.Data;
using HrOne_Rep.DTOs;
using HrOne_Rep.DTOs.Department;
using HrOne_Rep.IRepository;
using HrOne_Rep.Models;
using Microsoft.EntityFrameworkCore;

namespace HrOne_Rep.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    CreatedDate = d.CreatedDate
                }).ToListAsync();
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    CreatedDate = d.CreatedDate
                }).FirstOrDefaultAsync();
        }

        public async Task<Department> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                CreatedDate = dto.JoiningDate
            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task UpdateAsync(int id, DepartmentUpdateDto department)
        {
            var existingDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);

            if (existingDepartment != null)
            {
                existingDepartment.DepartmentName = department.DepartmentName;
                await _context.SaveChangesAsync();
            }
        }

        //public async Task DeleteAsync(int id)
        //{
        //    var department =
        //        await _context.Departments
        //            .FirstOrDefaultAsync(x =>
        //                x.DepartmentId == id);

        //    if (department != null)
        //    {
        //        _context.Departments.Remove(department);

        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
