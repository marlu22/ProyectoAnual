using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BusinessLogic.Services;
using BusinessLogic.Models;

namespace Services.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        // src/Services/Controllers/AuthController.cs (assumed snippet around line 36)
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // La contraseña ya viene encriptada desde el cliente
            var user = _userService.Authenticate(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            // Add null check for user.Rol
            var response = new
            {
                Username = user.Username,
                Rol = user.Rol ?? "Unknown",
                CambioContrasenaObligatorio = user.CambioContrasenaObligatorio
            };
            return Ok(response);
        }

        private string GenerateJwtToken(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");
            }

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured."));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}