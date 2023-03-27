using API.Dtos.Transaction.PackingScan;
using API.Dtos.Transaction.ReprintPackingScan;
using API.Helpers.Params.Transaction;
using SDCores;

namespace API._Services.Interfaces.Transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_ReprintPackingScanService
    {
        Task<PaginationUtility<ReprintPackingScanModel>> GetData(PaginationParam param, ReprintPackingScanParam filterParam);
        Task<List<PackingScanExportDto>> GetExportData(List<ReprintPackingScanModel> listTrNo);
    }
}