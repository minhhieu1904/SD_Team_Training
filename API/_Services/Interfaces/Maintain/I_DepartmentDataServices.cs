using API.DTOs.DepartmentDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_DepartmentDataServices
    {
        Task<PaginationUtility<MsDepartment>> GetData(PaginationParam pagination, DepartmentDataParam param);

        Task<MsDepartment> GetDataOnly(string manuf, string parNo);

        Task<OperationResult> Add(MsDepartment model);

        Task<OperationResult> Update(MsDepartment model);

        Task<OperationResult> Delete(string parNo);
    }
}