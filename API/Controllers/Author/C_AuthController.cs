
using System.Security.Claims;
using System.Text;

using API._Services.Interfaces;
using API.DTOs.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AgileObjects.AgileMapper;
using API.DTOs;

namespace API.Controllers.Author
{
    [Route("api/[controller]")]
    [ApiController]
    public class C_AuthController : ControllerBase
    {
        
        private readonly  IConfiguration _config;
        private readonly  IAuthorService _authorService;

        public C_AuthController(IConfiguration config,IAuthorService authorService )
        {
           
            _config = config;
            _authorService = authorService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userlogin)
        {
            // Lấy thông tin người dùng bao gồm tài khoản và mật khẩu
            //  Nếu như tài khoản và mật khẩu không trung khớp thì trả về null
            var userForm = await _authorService.Login(userlogin);
            // Khi đó sẽ trả ra không  có quyền đăng nhập (status : 401) không cho phép đăng nhập
            if(userForm == null) return Unauthorized();
            // khai báo thông tin lưu trữ vào jwt token

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userForm.account.ToString()),
                new Claim(ClaimTypes.Name, userForm.name)
            };
            
            // Lấy mã Token tại AppSetting, 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            // tạo thông tin đăng nhập được mã hoá theo key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            // Tạo thông tin Claims với ngày hết hạn và thông tin người dùng
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            // Sinh token để trả về kèm theo thông tin Claims

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
               token = tokenHandler.WriteToken(token),
               user = userForm 
            });
        }
    }
}