using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API._Services.Interfaces;
using API.Helpers.Params.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration config, IAuthService authService)
        {
            _config = config;
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginParam param)
        {
            //Lấy thông tin người dùng bao gồm tài khoản và mật khẩu
            // Nếu như tài khoản và mật khẩu không trùng khớp thì trả về null
            var userForm = await _authService.Login(param);
            // Khi đó sẽ trả về không có quyền đăng nhập (status: 401) không cho phép đăng nhập
            if (userForm == null) return Unauthorized();

            //Khai báo thông tin lưu trữ vào jwt token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userForm.Id.ToString()),
                new Claim(ClaimTypes.Name, userForm.Name)
            };

            // Lấy mã Token tại AppSetting,
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));

            //tạo thông tin đăng nhập được mã hóa theo key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Tạo thông tin Claims với ngày hết hạn và thông tin người dùng
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //Sinh token để trả về kèm theo thông tin Claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = userForm
            });
        }
    }
}