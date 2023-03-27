using API.Dtos.Auth;
using API.Helpers.Params.Login;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IAuthService
    {
        Task<UserForLoggedDTO> Login(UserLoginParam userParamLogin);
    }
}