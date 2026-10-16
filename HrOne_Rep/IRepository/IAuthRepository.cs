using HrOne_Rep.DTOs;
using HrOne_Rep.Models;

namespace HrOne_Rep.IRepository
{
    public interface IAuthRepository
    {
        User? Login(LoginRequestDto request);
        bool Register(RegisterRequestDto request);
    }
}
