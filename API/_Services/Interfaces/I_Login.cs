using API.DTOs.Login;

namespace API._Services.Interfaces
{
    public interface I_Login
    {
        Task<UserLoginDTO> Login(UserLogin login);
    }
}