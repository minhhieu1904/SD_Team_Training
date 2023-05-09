using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.PackingScan;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IPackingScanService
    {
        Task<OperationResult> SavePackingScanList(PackingScanDTO data, string userName);
        Task<OperationResult> CheckScanItem(string scanText);
        Task<PaginationUtility<ViewDataPackingScan>> GetList(PaginationParam paginationParam, string TransactionNo);
        Task<List<PackingScanExportDTO>> GetDataExport(string TransactionNo);
    }
}