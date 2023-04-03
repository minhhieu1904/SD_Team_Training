using API.DTOs.Login;
using API.Models;

namespace API._Services.Interfaces
{
    public interface I_LoginServices
    {
        Task<UserLoginDTO> Login(UserLogin  userLogin);
    }
}