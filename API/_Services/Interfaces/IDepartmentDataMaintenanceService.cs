using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IDepartmentDataMaintenanceService
    {
        Task<PaginationUtility<MS_Department>> GetAll(PaginationParam pagination, DepartmentDataMaintenanceParam param);
        Task<OperationResult> Create (MS_Department model);
        Task<OperationResult> Update (MS_Department model);
        Task<PaginationUtility<MS_Department>> Search (PaginationParam pagination, string text);
    }
}