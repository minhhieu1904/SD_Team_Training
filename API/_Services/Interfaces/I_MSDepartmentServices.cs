using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IMSDepartmentServices
    {
        Task<PaginationUtility<MS_Department>> GetPagingData(PaginationParam param, string parNo, string parName);
        Task<OperationResult> AddNew(MS_Department model);
        Task<OperationResult> Update(MS_Department model);
        Task<MS_Department> GetItem(string manuf, string parNo);
    }
}