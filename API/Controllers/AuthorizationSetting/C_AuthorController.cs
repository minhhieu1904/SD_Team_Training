
using System.Text;
using System.Security.Claims;
using API._Services.Interfaces;
using API.DTOs.userLogin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers.AuthorizationSetting
{
    public class C_AuthorController : APIController
    {
        private readonly IConfiguration _config;
        private readonly IAuthorService _authorService; 

        public C_AuthorController(IConfiguration config,IAuthorService  authorService){
            _config = config;
            _authorService = authorService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(userLogin userLogin){
            //Lấy thông tin người dùng bao gồm tài khoản và mật khẩu
            // Nếu như tài khoản và mật khẩu không trùng khớp thì trả về null
            var userform = await _authorService.Login(userLogin);
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