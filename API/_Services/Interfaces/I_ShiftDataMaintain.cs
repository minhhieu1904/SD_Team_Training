using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.ShiltDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_ShiftDataMaintain
    {
        Task<PaginationUtility<MS_Shift>> GetData(PaginationParam pagination ,ShiftDataMaintainParam param);
        Task<OperationResult> Addnew(MS_Shift model);
        Task<OperationResult> Update(MS_Shift model);

        Task<MS_Shift> GetDataOnly(string manuf, string shift);
        Task<OperationResult> Delete(MS_Shift model);

    }
}