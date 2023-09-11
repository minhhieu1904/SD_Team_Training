using API.DTOs.Role;
using API.Helper.Params;
using API.Models;
using SDCores;
namespace API._Services.Interfaces
{
    // Bước 5 : thêm dòng dưới cho tất cả  interface
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IAuthorizationSettingService
    {
        Task<PaginationUtility<User>> GetAll(PaginationParam pagination, AuthorizationSettingParam param);
        Task<OperationResult> Create(UserFormParam model);
        Task<OperationResult> Update(UserFormParam model);
        Task<UserRoleDTO> GetAllRoleByAccount(string account);
        Task<OperationResult> UpdateRoleByAccount(UserRoleDTO model);
    }
}