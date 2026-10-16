using System.ComponentModel.DataAnnotations;

namespace HrOne_Rep.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        // ✅ ADDED
        // One Department -> Many Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
