using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API._Services.Interfaces;
using API.Helpers.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : APIController
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginParam param)
        {
            var userForLogin = await _authService.Login(param);
            if (userForLogin == null)
            {
                return Unauthorized();
            }
            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, userForLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, userForLogin.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMonths(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = userForLogin
            });
        }
    }
}