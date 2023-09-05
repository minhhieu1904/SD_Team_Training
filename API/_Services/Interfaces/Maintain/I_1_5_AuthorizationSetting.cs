using API.Dtos.Maintain.AuthorizationSetting;
using API.DTOs.Maintain;
using SDCores;

namespace API._Services.Interfaces.Maintain
{
    public interface I_1_5_AuthorizationSetting
    {
        public Task<PaginationUtility<AuthorizationDto>> GetDataPagination(PaginationParam pagination, AuthorizationParam param);
        public Task<OperationResult> AddNew(AuthorizationDto data);
        public Task<OperationResult> Edit(AuthorizationDto data);
        public Task<List<RolesUserStatus>> GetRoleUser(string account);
        public Task<bool> EditRoleUser(List<RolesUserStatus> roles, string updateBy);
    }
}