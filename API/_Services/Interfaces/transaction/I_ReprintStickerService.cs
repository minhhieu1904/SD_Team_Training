using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Transaction.ReprintSticker;
using API.Helpers.Params.Transaction;
using SDCores;

namespace API._Services.Interfaces.transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_ReprintStickerService
    {
        Task<PaginationUtility<ReprintStickerModel>> GetData(PaginationParam pagination, ReprintStickerParam filterParam);
        Task<bool> UpdateData(List<ReprintStickerModel> data);
        Task<List<ReprintStickerModel>> GetDataByScan(List<ReprintStickerModel> listModel);
    }
}