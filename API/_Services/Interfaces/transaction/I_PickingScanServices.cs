using API.Dtos.Transaction.PickingScan;
using API.Helpers.Params.Transaction;
using SDCores;

namespace API._Services.Interfaces.Transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_PickingScanServices
    {
        Task<PaginationUtility<PickingMainDto>> GetData(PaginationParam pagination, PickingScanParam filterParam);
        Task<GetScanPickingMainDto> GetDataByScan(string QRCodeValue);
        Task<GetScanPickingMainDto> GetDataDetail(PickingScanParam param);
        Task<OperationResult> Update(GetScanPickingMainDto listPickingDetail, string userName);
        Task<OperationResult> Release(List<PickingMainDto> releasePickingMain, string currentUser);
        Task<List<DataExportExcel>> ExportExcel(List<PickingMainDto> dataPickingMain);
        Task<string> GenerateCodeDeclarationNo();

        #region Picking 
        Task<OperationResult> CheckPickingScanCode(PickingScanParam param, string scanCode);
        Task<GetScanPickingMainDto> GetDataByScanCode(PickingScanParam param, string scanCodes);
        Task<OperationResult> UpdatePickingQrCode(GetScanPickingMainDto data, PickingScanParam param);

        #endregion
    }
}