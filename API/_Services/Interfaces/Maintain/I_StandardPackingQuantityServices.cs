using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Maintain.StandardPackingQuantity;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_StandardPackingQuantityServices
    {
        Task<PaginationUtility<MsPackage>> GetData(PaginationParam pagination, StandardPackingQuantityParam param);
        Task<MsPackage> GetDataOnly(string manuf, string packageNo);
        Task<OperationResult> Add(MsPackage model);
        Task<OperationResult> Update(MsPackage model);

        
    }
}