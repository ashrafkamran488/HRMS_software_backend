using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel.DataAnnotations;

namespace HrOne_Rep.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }

        // ✅ FK COLUMN
        public int DepartmentId { get; set; }

        // ✅ NAVIGATION PROPERTY
        public Department? Department { get; set; }
    }
}
