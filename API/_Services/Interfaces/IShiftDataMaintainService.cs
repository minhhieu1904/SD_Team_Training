using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.ShiftDataMaintain;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IShiftDataMaintainService
    {
        Task<MS_Shift> GetItem(string manuf, string shift);
        Task<PaginationUtility<MS_Shift>> GetAll(PaginationParam pagination, ShiftDataMaintainParam param); 
        Task<OperationResult> Create(MS_Shift msshift);
        Task<OperationResult> Update(MS_Shift msshift);

    }
}