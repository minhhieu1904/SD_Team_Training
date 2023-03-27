using API.Dtos;
using API.Dtos.Maintain.AuthorizationSetting;
using API.Models;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IAuthorizationSettingService
    {
        Task<PaginationUtility<Users>> GetDataUsers(PaginationParam pagination);
        Task<List<RolesUserStatus>> GetRoleUser(string account);
        Task<bool> UpdateAuthorUser(List<RolesUserStatus> authors, string userUpdate);
        Task<OperationResult> Add(UserDto userDto);
        Task<OperationResult> Edit(UserDto userDto);
    }
}