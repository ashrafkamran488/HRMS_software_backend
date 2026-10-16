using HrOne_Rep.DTOs;
using HrOne_Rep.IRepository;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HrOne_Rep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDto request)
        {
            bool result = _authRepository.Register(request);

            if (result)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "User Created Successfully"
                });
            }

            return BadRequest(new
            {
                Success = false,
                Message = "User Creation Failed"
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            var user = _authRepository.Login(request);

            if (user == null)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Invalid Username or Password"
                });
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.RoleName),
        new Claim("Id", user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: credentials);

            string jwtToken =
                new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                Success = true,
                Token = jwtToken,
                UserName = user.UserName,
                RoleName = user.RoleName
            });
        }


    }
}
