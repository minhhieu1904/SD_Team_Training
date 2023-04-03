using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("[controller]")]
    public class C_Login : APIController
    {
        private readonly IConfiguration _config;
        private readonly I_Login _login;

        public C_Login(IConfiguration config, I_Login login)
        {
            _config = config;
            _login = login;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin){
            //Lấy thông tin người dùng bao gồm tài khoản và mật khẩu
            // Nếu như tài khoản và mật khẩu không trùng khớp thì trả về null
            var userform = await _login.Login(userLogin);
            // Khi đó sẽ trả về không có quyền đăng nhập (status: 401) không cho phép đăng nhập
            if(userform==null) return Unauthorized();
            //Khai báo thông tin lưu trữ vào jwt token
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userform.account.ToString()),
                new Claim(ClaimTypes.Name, userform.name)
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
                User = userform
            });
        }
    }
}