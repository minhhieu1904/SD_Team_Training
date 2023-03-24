using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD3_API.Helpers.Utilities;
using API.Models;
using API.DTOs.ShiftDataMaintain;

namespace API._Services.Interfaces
{
    public interface I_ShiftDataMaintainServices
    {
        Task<PaginationUtility<MsShift>> GetData(PaginationParam pagination, ShiftDataMaintainParam param);

        Task<OperationResult> Add(MsShift model);

        Task<OperationResult> Update(MsShift model);

        Task<OperationResult> Delete(string shift);

        Task<MsShift> GetDataOnly(string manuf, string shift);
    }
}