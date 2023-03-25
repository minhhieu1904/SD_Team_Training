using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_StandardPackingQuantityServices
    {
        Task<PaginationUtility<MsPackage>> GetData(PaginationParam pagination, StandardPackingQuantityParam param);

        Task<MsPackage> GetDataOnly(string manuf, string packageNo);

        Task<OperationResult> Add(StandardPackingQuantityParam model);

        Task<OperationResult> Update(StandardPackingQuantityParam model);

        Task<OperationResult> Delete(string packageNo);
    }
}