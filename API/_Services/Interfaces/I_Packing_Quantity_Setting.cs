using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_Packing_Quantity_Setting
    {
        Task<OperationResult> Create(MS_Package_DTO dto); 
        Task<PaginationUtility<MS_Package_DTO>> Search(PaginationParam pagination, MS_Package_DTO dto);
        Task<OperationResult> Update(MS_Package_DTO dto);
        Task<MS_Package_DTO> GetDetail(MS_Package_DTO dto);
    }
}