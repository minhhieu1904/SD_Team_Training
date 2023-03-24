using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IUsersServices
    {
        Task<PaginationUtility<Users>> GetDataPaing(PaginationParam param, string account, string name);
        Task<OperationResult> AddNew(Users model);
        Task<OperationResult> Update(Users model);
        Task<Users> GetItem(string account, string name);

        //Dach sách quyền theo Account của User
        Task<UserRoleDTO> GetAllRoleByAccount(string account);
        Task<OperationResult> UpdateAuthorization(UserRoleDTO authors);
    }
}