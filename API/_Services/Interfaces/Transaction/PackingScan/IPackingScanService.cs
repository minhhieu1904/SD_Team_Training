using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Transaction.PackingScan;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Transaction.PackingScan
{
    public interface IPackingScanService
    {
        Task<OperationResult> CheckScanItem(string scanText);
        Task<PaginationUtility<PackingScanViewDTO>> GetList(PaginationParam paginationParam, string TransactionNo);
        Task<OperationResult> SavePackingScanList(PackingScanDTOParam data, string userName);
        Task<List<PackingScanExportDTO>> GetDataExport(string TransactionNo);
        Task<List<KeyValuePair<string, string>>> getListShift();
    }
}