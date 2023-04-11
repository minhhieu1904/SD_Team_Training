using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Role;
using API.Helper.Params;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IAuthorizationSettingService
    {
        Task<PaginationUtility<User>> GetAll(PaginationParam pagination, AuthorizationSettingParam param);
        Task<OperationResult> Create(UserFormParam model);
        Task<OperationResult> Update(UserFormParam model);
        Task<UserRoleDTO> GetAllRoleByAccount (string account);
        Task<OperationResult> UpdateRoleByAccount (UserRoleDTO model);
    }
}