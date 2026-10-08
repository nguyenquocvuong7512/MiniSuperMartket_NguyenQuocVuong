using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _context;

        public AuthController(
            IConfiguration configuration,
            SupermarketDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Vui lòng nhập tài khoản và mật khẩu!"
                });
            }

            // Tìm tài khoản và lấy Role
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Username == request.Username);

            // Không tồn tại tài khoản hoặc sai mật khẩu
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            // Kiểm tra Role
            if (user.Role == null)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Tài khoản chưa được cấp vai trò!"
                });
            }

            string role = user.Role.RoleName;

            // Tạo JWT
            var token = GenerateJwtToken(
                user.Username,
                role);

            return Ok(new
            {
                success = true,
                token = token,
                username = user.Username,
                fullName = user.FullName,
                role = role
            });
        }

        private string GenerateJwtToken(
            string username,
            string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(
                _configuration["JwtSettings:Secret"]
                ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!"
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(
                            ClaimTypes.Name,
                            username),

                        new Claim(
                            ClaimTypes.Role,
                            role)
                    }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(
                tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}