namespace HrOne_Rep.DTOs
{
    public class EmployeeDetailsDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNo { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
    }
}
