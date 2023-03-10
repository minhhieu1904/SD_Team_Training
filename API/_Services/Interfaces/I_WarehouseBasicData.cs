using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.WarehouseBasicData;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_WarehouseBasicData
    {
        Task<PaginationUtility<MS_Location>> GetData(PaginationParam pagination ,WarehouseBasicDataParam param);
        Task<OperationResult> Addnew(MS_Location model);
        Task<OperationResult> Update(MS_Location model);

        Task<MS_Location> GetDataOnly(string manuf, string StoreH);
        Task<OperationResult> Delete(MS_Location model);
    }
}