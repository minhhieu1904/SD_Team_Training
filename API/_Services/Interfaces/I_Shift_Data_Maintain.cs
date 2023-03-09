using API.DTOs;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_Shift_Data_Maintain
    {
        Task<OperationResult> Create(MS_Shift_DTO dto);
        Task<OperationResult> Update(MS_Shift_DTO dto);
        Task<PaginationUtility<MS_Shift_DTO>> Search(PaginationParam pagination, MS_Shift_DTO dto);
        Task<MS_Shift_DTO> GetDetail( MS_Shift_DTO dto);// Mình thêm hàm lấy chi tiết ở đây thì bên Service cũng phải có không là nó chửi
    }
}