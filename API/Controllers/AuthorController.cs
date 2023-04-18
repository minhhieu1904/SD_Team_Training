using System.Text;
using System.Security.Claims;
using API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Author;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : APIController
    {
        private readonly IConfiguration _config;
        private readonly IAuthorService _service;
        public AuthorController(IAuthorService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }
        
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var userForLogin = await _service.Login(userLogin);

            if (userForLogin == null)
                return Unauthorized();

            // khai báo lưu trữ vào jwt token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userForLogin.account.ToString()),
                new Claim(ClaimTypes.Name, userForLogin.name),

            };
            // Lấy mã token tại appsettings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            // tạo thông tin đăng nhập được mã hóa theo key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Tạo thông tin Claims với ngày hết hạn và thông tin người dùng
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            //Sinh token để trả về kèm theo thông tin Claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = userForLogin
            });
        }
    }
}