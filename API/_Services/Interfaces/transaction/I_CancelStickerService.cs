using API.Dtos.Transaction.CancelSticker;
using API.Helpers.Params.Transaction;
using SDCores;

namespace API._Services.Interfaces.Transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_CancelStickerService
    {
        Task<OperationResult> CheckRecordScan(string qrCodeValue);
        Task<OperationResult> CancelStickerScan(List<string> datas, string userName);
        Task<OperationResult> CancelStickerAction(List<string> datas, string userName);
        Task<PaginationUtility<CancelStickerViewModel>> GetDataPagination(PaginationParam pagination, CancelStickerParam param, bool isPaging = true);
    }
}