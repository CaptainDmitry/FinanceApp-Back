using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs;
using TestApi.Helpers;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TestContext _context;
        public AuthenticationController(TestContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {         
            var user = _context.users.FirstOrDefault(e => e.Email == login.Email);
            if (user == null) return Unauthorized("Неверный email/пароль");
            if (!user.VerifHashPassword(login.Password)) return Unauthorized("Неверный email/пароль");
            var token = JwtHelper.GenerateJwtToken(user.Id.ToString(), user.Name, "Admin");
            return Ok(new { message = "Авторизация успешна.", Token = token });
        }

        //POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest newUser)
        {
            if (_context.users.Any(x => x.Email == newUser.Email)) return BadRequest("Пользователь с таким email уже существует.");

            var user = new User
            {
                Name = newUser.Username,
                Email = newUser.Email,
                PasswordHash = newUser.Password
            };
            _context.users.Add(user);
            _context.SaveChangesAsync();
            return Ok(new { message = "Регистрация прошла успешно" });
        }

    }
}
