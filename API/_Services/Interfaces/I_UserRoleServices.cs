
using API.Models;
using API.DTOs;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_UserRoleServices
    {
        Task<PaginationUtility<Users>> Search(PaginationParam paginationParams, string account, string name);
        Task<OperationResult> Create(UserDTO user);
        Task<OperationResult> Update(UserDTO user);
        Task<UserRoleDTO> GetAllRoleByAccount(string account);
        Task<OperationResult> UpdateAuthorization(UserRoleDTO authors);
    }
}
