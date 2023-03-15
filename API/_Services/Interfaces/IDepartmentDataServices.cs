using API.Models;
using SD3_API.Helpers.Utilities;


namespace API._Services.Interfaces
{
    public interface IDepartmentDataServices
    {
        Task<PaginationUtility<MS_Department>> LoadData(PaginationParam paginationParams, string ParNo, string ParName);
        Task<OperationResult> Add(MS_Department ParNo);
        Task<OperationResult> Update(MS_Department ParNo);
    }
}