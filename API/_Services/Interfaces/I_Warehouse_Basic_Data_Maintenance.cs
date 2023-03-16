using API.DTOs;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_Warehouse_Basic_Data_Maintenance
    {
        Task<OperationResult> Create(MS_Location_DTO dto);
        Task<OperationResult> Update(MS_Location_DTO dto);
        Task<PaginationUtility<MS_Location_DTO>> Search(PaginationParam pagination, MS_Location_DTO dto);
        Task<MS_Location_DTO> GetDetail( MS_Location_DTO dto);
    }
}