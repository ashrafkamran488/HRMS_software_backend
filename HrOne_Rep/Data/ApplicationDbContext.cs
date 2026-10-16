using HrOne_Rep.Models;
using Microsoft.EntityFrameworkCore;

namespace HrOne_Rep.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Note> Note { get; set; }

    }
}
