using API.Models;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IMSShiftServices
    {

        Task<List<MS_Shift>> GetAllShift();

        // Lấy danh sách Shift theo phân trang 
        Task<PaginationUtility<MS_Shift>> GetDataPagination(PaginationParam param, string shift, string shiftName);
        // Thêm mới Shift
        Task<OperationResult> AddNewShift(MS_Shift model);
        // Chỉnh sửa Shift
        Task<OperationResult> UpdateShift(MS_Shift model);
        // Xóa Shift
        Task<OperationResult> Delete(MS_Shift model);
        // Lấy item theo khóa chính Manuf và Shift
        Task<MS_Shift> GetItem(string manuf, string shift);
    }
}