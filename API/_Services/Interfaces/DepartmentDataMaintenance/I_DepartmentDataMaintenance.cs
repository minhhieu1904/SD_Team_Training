using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_DepartmentDataMaintenance
    {
        Task<PaginationUtility<MS_Department>> GetData(PaginationParam pagination ,DepartmentDataMaintenanceParam param);
        Task<OperationResult> Addnew(MS_Department model);
        Task<OperationResult> Update(MS_Department model);

        Task<MS_Department> GetDataOnly(string manuf, string StoreH);
        
    }
}