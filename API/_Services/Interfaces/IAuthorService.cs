using API.DTOs.Author;
using SDCores;
namespace API._Services.Interfaces
{ 
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IAuthorService
    {
        Task<UserLoginDTO> Login (UserLogin userLogin);
    }
}