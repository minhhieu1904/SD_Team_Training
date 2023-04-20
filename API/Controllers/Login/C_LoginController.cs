using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_LoginController : ControllerBase
    {
        private readonly I_LoginServices _services;
        private readonly IConfiguration _config;
        public C_LoginController(I_LoginServices services,IConfiguration config)
        {
            _services = services;
            _config = config;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin){
            var user = await _services.Login(userLogin);
            // Khi đó sẽ trả về không có quyền đăng nhập (status: 401) không cho phép đăng nhập

            if(user==null) return Unauthorized();
            //Khai báo thông tin lưu trữ vào jwt token

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.Account.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
             // Lấy mã Token tại AppSetting,
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //Tạo thông tin Claims với ngày hết hạn và thông tin người dùng

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            //Sinh token để trả về kèm theo thông tin Claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token = tokenHandler.WriteToken(token),
                User = user
            });
        }
    }
}