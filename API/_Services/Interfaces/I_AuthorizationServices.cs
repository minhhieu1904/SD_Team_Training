using SD3_API.Helpers.Utilities;
using API.Models;
using API.DTOs.AuthorizationSetting;

namespace API._Services.Interfaces
{
    public interface I_AuthorizationServices
    {
        public Task<PaginationUtility<User>> GetData(PaginationParam pagination, string account);

        public Task<User> GetDataOnly(string account);

        public Task<OperationResult> Add(User model);

        public Task<OperationResult> Update(User model);

        public Task<List_RoleUserParam> GetAllRoleByAccount(string account);

        public Task<OperationResult> UpdateAuthorization(List_RoleUserParam userRoleParam);
    }
}