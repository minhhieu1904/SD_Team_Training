
using API.Models;
using API.DTOs;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IUserRoleServices
    {
        Task<PaginationUtility<Users>> LoadData(PaginationParam paginationParams, string account, string name);
        Task<OperationResult> Add(UserDTO user);
        Task<OperationResult> Update(UserDTO user);


        // Danh sách quyền theo Account của User
        Task<UserRoleDTO> GetAllRoleByAccount(string account);

        // Cập nhật phân quyền theo Account
        Task<OperationResult> UpdateAuthorization(UserRoleDTO authors);

    }



}
