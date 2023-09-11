
using API.DTOs.ShiftDataMaintain;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SDCores;
namespace API._Services.Interfaces
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IShiftDataMaintainService
    {
        Task<PaginationUtility<MS_Shift>> GetDataPagination(PaginationParam pagination, ShiftDataMaintainParam param);
        Task<OperationResult> Create(MS_ShiftDTO data);
        Task<OperationResult> Update(MS_ShiftDTO data);
        Task<OperationResult> Delete(MS_ShiftDTO data);
        public Task<byte[]> DownloadFileExcel(ShiftDataMaintainParam param);
        Task<OperationResult> UploadFileExcel(ShiftDataMaintainUploadParam param, string userName);
    }
}