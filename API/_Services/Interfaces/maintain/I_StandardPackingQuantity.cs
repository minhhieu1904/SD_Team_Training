using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_StandardPackingQuantity
    {
        Task<PaginationUtility<MS_Package>> GetData(PaginationParam pagination ,StandardPackingQuantityParam param);
        Task<OperationResult> Addnew(StandardPackingQuantityAddParam model);
        Task<OperationResult> Update(StandardPackingQuantityAddParam model);

        Task<MS_Package> GetDataOnly(string manuf, string PackageNo);
        
    }
}