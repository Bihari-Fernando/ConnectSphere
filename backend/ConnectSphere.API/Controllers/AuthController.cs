using ConnectSphere.API.DTOs;
using ConnectSphere.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnectSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (result == null)
                return BadRequest(new { message = "Email already exists" });

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(result);
        }
    }
}