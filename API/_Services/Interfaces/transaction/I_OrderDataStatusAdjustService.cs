
using API.Dtos.Transaction.OrderDataStatusAdjust;
using SDCores;

namespace API._Services.Interfaces.transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_OrderDataStatusAdjustService
    {
        Task<OrderDataStatusAdjustResponse> GetDataPagination(PaginationParam param, OrderDataStatusAdjustParam filter);
        // Cập nhật đơn hàng
        Task<OperationResult> Update(OrderDataStatusAdjustUpdate model);
    }
}