using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
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
    }
}