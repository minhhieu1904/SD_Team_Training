using API.Dtos.Auth;
using API.Helpers.Login;

namespace API._Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserForLoggedDTO> Login(UserLoginParam userForLogin);
    }
}