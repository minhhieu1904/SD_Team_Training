using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IMSPackageServices
    {
        Task<PaginationUtility<MS_Package>> GetDataPaing(PaginationParam param, string packageNo, decimal packageQty);
        Task<OperationResult> Add(MS_Package model);
        Task<OperationResult> Update(MS_Package model);
        Task<MS_Package> GetItem(string manuf, string packageNo);
    }
}