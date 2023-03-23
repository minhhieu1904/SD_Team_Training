using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}