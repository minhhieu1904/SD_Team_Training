using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IShiftDataMaintenanceService
    {
        Task<PaginationUtility<MS_Shift>> LoadData(PaginationParam paginationParams, string shift, string shiftName);
        Task<OperationResult> Add(MS_Shift shift);
        Task<OperationResult> Update(MS_Shift shift);
     
    }
}