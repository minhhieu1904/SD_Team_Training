using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IWarehouseBasicDataMaintenanceService
    {
        Task<PaginationUtility<MS_Warehouse>> GetAll(PaginationParam pagination, WarehouseBasicDataMaintenanceParam param);
        Task<OperationResult> Create(MS_Warehouse model);
        Task<OperationResult> Update(MS_Warehouse model);
        Task<PaginationUtility<MS_Warehouse>> Search(PaginationParam param, string text);
    }
}