using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.userLogin;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_AuthorizationSetting
    {
        Task<PaginationUtility<Users>> GetData(PaginationParam pagination ,string account, string name);
        Task<OperationResult> Addnew(Users model);
        Task<OperationResult> Update(Users model);

        Task<Users> GetDataOnly(string manuf);
        Task<UserRoleDTO> GetAllRoleByAccount(string account);

        // Cập nhật phân quyền theo Account
        Task<OperationResult> UpdateAuthorization(UserRoleDTO authors);
    }
}