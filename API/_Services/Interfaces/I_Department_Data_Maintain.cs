using API.DTOs;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_Department_Data_Maintain
    {
        Task<OperationResult> Create(MS_Department_DTO dto);
        Task<PaginationUtility<MS_Department_DTO>> Search(PaginationParam pagination, MS_Department_DTO dto);
        Task<OperationResult> Update(MS_Department_DTO dto);
        Task<MS_Department_DTO> GetDetail(MS_Department_DTO dto);
    }
}