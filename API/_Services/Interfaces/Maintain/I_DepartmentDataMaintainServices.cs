using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Maintain.DepartmentDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_DepartmentDataMaintainServices
    {
        Task<PaginationUtility<MsDepartment>> GetData(PaginationParam pagination, DepartmentDataMaintainParam param);
        Task<MsDepartment> GetDataOnly(string manuf, string parNo);
        Task<OperationResult> Add(MsDepartment model);
        Task<OperationResult> Update(MsDepartment model);
    }
}