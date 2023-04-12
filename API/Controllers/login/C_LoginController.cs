using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API._Services.Interfaces.Login;
using API.DTOs.userLogin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    public class C_LoginController : APIController
    {
        private readonly I_LoginServices _services;
        private readonly IConfiguration _config;

        public C_LoginController(I_LoginServices services,IConfiguration config)
        {
            _services = services;
            _config = config;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(userLogin userLogin){
            //Gán thông tin người dùng từ method Login() bên Services vào biến userfrom
            var userform = await _services.Login(userLogin);

            // Khi đó sẽ trả về không có quyền đăng nhập (status: 401) không cho phép đăng nhập
            if(userform==null) return Unauthorized();

            // Nếu thông tin người dùng hợp lệ, chúng ta tạo các claims (thông tin trade) và mã 
            // hóa thông tin này thành token JWT. Lưu ý biến _config được sử dụng để lấy giá trị cho mã thông báo symmetrical
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userform.account.ToString()),
                new Claim(ClaimTypes.Name, userform.name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{ 
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(60),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token = tokenHandler.WriteToken(token),
                User = userform
            });
        }


    }
}