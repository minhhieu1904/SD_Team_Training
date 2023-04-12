using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Maintain.WareHouseBasicData;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_WareHouseBasicDataServices
    {
        Task<PaginationUtility<MsLocation>> GetData (PaginationParam pagination, WareHouseBasicDataParam param);
        Task<OperationResult> Add(MsLocation model);
        Task<OperationResult> Update(MsLocation model);
        Task<MsLocation> GetDataOnly (string manuf, string StoreH);        
        
    }
}