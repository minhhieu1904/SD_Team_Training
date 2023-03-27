using API.DTOs;
using API.DTOs.Author;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IAuthorService
    {
        Task<UserLoginDto> Login(UserLogin userLogin);
        Task<OperationResult> changePassword(UserPassword user);
    }
}