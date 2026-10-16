namespace HrOne_Rep.DTOs
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
