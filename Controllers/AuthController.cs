using DoctorAppointment.API.Data;
using DoctorAppointment.API.DTOs;
using DoctorAppointment.API.Models;
using DoctorAppointment.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        private readonly ApplicationDbContext _dbContext;

        public AuthController(AuthService authService, ApplicationDbContext dbContext)
        {
            _authService = authService;
            _dbContext = dbContext;
        }


        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = "User"
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email && u.PasswordHash == dto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = _authService.GenerateToken(user);
            return Ok(new { token = token, role = user.Role });
        }
    }
}
