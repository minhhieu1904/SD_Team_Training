using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_AuthorizationServices
    {
        Task<PaginationUtility<User>> GetData(PaginationParam pagination, string account);
        Task<User> GetDataOnly(string account);
        Task<OperationResult> Add(User model);
        Task<OperationResult> Update(User model);
        Task<RoleUserParam> GetAllRoleByAccount(string account);
        Task<OperationResult> UpdateAuthorization(RoleUserParam userRoleParam);
    }
}