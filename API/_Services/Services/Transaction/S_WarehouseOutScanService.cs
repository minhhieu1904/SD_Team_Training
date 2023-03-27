using API._Repositories;
using API._Services.Interfaces.transaction;
using API.Data;
using API.Dtos.Transaction.PickingScan;
using API.Dtos.Transaction.WarehouseOutScan;
using API.Helpers.Params.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_WarehouseOutScanService : I_WarehouseOutScanService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;

        public S_WarehouseOutScanService(IRepositoryAccessor repositoryAccessor, IConfiguration configuration, DBContext context)
        {
            _repositoryAccessor = repositoryAccessor;
            _configuration = configuration;
            _context = context;
        }

        public async Task<PaginationUtility<MS_QR_PickingMain>> GetDataMain(PaginationParam pagination, WarehouseOutScanParam param)
        {
            var pred = PredicateBuilder.New<MS_QR_PickingMain>(true);
            if (!string.IsNullOrWhiteSpace(param.DeclarationNo))
                pred.And(x => x.declaration_no.Trim() == param.DeclarationNo.Trim());
            if (!string.IsNullOrWhiteSpace(param.Invno))
                pred.And(x => x.invno.Trim() == param.Invno.Trim());
            if (!string.IsNullOrWhiteSpace(param.Iono))
                pred.And(x => x.iono.Trim() == param.Iono.Trim());
            if (!string.IsNullOrWhiteSpace(param.ReleaseStartDate) && !string.IsNullOrWhiteSpace(param.ReleaseEndDate))
                pred.And(x => x.ReleaseDate >= Convert.ToDateTime(param.ReleaseStartDate).Date && x.ReleaseDate <= Convert.ToDateTime(param.ReleaseEndDate).Date);
            if (!string.IsNullOrWhiteSpace(param.Manno))
                pred.And(x => x.manno.Trim() == param.Manno.Trim());
            if (!string.IsNullOrWhiteSpace(param.Size))
                pred.And(x => x.size.Trim() == param.Size.Trim());

            var data = _repositoryAccessor.MS_QR_PickingMain.FindAll(pred);
            return await PaginationUtility<MS_QR_PickingMain>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<PaginationUtility<PickingDetailDto>> GetPickingFromMainWarehouse(PaginationParam pagination, PickingScanItemParam param)
        {
            var pred = PredicateBuilder.New<MS_QR_PickingDetail>(true);
            pred.And(x => x.iono == param.Iono
                    && x.manno == param.ManNo
                    && x.purno == param.PurNo
                    && x.size == param.Size);
            var pickingDetail = _repositoryAccessor.MS_QR_PickingDetail.FindAll().AsNoTracking();
            var pickingMain = _repositoryAccessor.MS_QR_PickingMain.FindAll().AsNoTracking();
            var msQRStorage = _repositoryAccessor.MS_QR_Storage.FindAll().AsNoTracking();

            var data = await pickingDetail
            .GroupJoin(
                pickingMain,
                x => new { x.manuf, x.iono, x.manno, x.purno, x.size },
                y => new { y.manuf, y.iono, y.manno, y.purno, y.size },
                (x, y) => new { T1 = x, T2 = y }
            ).SelectMany(x => x.T2.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = y })
            .GroupJoin(
                msQRStorage,
                x => new { x.T1.manuf, x.T1.manno, x.T1.purno, x.T1.size },
                y => new { y.manuf, y.manno, y.purno, y.size },
                (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y }
            ).SelectMany(x => x.T3.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y })
            .Select(x => new PickingDetailDto
            {
                Sdat = x.T1.Sdat,
                declaration_no = x.T2.declaration_no,
                storageTrNo = x.T3.trno,
                Grade = x.T1.Grade,
                QRCodeID = x.T1.QRCodeID,
                manno = x.T1.manno,
                purno = x.T1.purno,
                size = x.T1.size,
                qty = x.T1.qty
            }).AsNoTracking().ToListAsync();

            return PaginationUtility<PickingDetailDto>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<OperationResult> StorageOut(List<MS_QR_PickingMain> listStorageOut, string currentUser)
        {
            if (!listStorageOut.Any())
                return new OperationResult(false);
            var listIono = listStorageOut.Select(x => x.iono).Distinct().ToList();
            List<MS_QR_PickingMain> pickingMainUpdateList = new List<MS_QR_PickingMain>();
            List<MS_QR_Storage> storageUpdateList = new List<MS_QR_Storage>();

            foreach (var data in listIono)
            {
                var pickingMains = await _repositoryAccessor.MS_QR_PickingMain.FindAll(x => x.iono.Trim() == data.Trim()).ToListAsync();

                var checkCondition = pickingMains.Where(item => (item.ExpectQTY != item.ExpectQTY) || item.status != "Release").ToList();
                if (checkCondition.Any())
                    return new OperationResult(false, "IonoCanNotStorageOut");

                foreach (var item in pickingMains)
                {
                    item.status = "StorageOut";
                    item.updated_by = currentUser;
                    item.update_time = DateTime.Now;
                    pickingMainUpdateList.Add(item);
                }
                var query = await _repositoryAccessor.MS_QR_PickingDetail.FindAll(x => x.iono.Trim() == data.Trim()).ToListAsync();
                if (!query.Any())
                    return new OperationResult(false, "EmptyQRCode");
                foreach (var pickingDetail in query)
                {
                    var storage = await _repositoryAccessor.MS_QR_Storage.FirstOrDefaultAsync(x => x.QRCodeID.Trim() == pickingDetail.QRCodeID.Trim());
                    if (storage == null || storage.IsStorageOut == "Y")
                        return new OperationResult(false, "InvalidQRCodeandIsStorageOutinStorage");
                    storage.IsStorageOut = "Y";
                    storageUpdateList.Add(storage);
                }

            }
            using (var transaction = await _repositoryAccessor.BeginTransactionAsync())
                try
                {
                    _repositoryAccessor.MS_QR_PickingMain.UpdateMultiple(pickingMainUpdateList);
                    _repositoryAccessor.MS_QR_Storage.UpdateMultiple(storageUpdateList);
                    await _repositoryAccessor.Save();
                    await transaction.CommitAsync();
                    return new OperationResult(true);
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return new OperationResult(false);
                }

        }

        // public async Task<List<PickingDetailOutExportData>> ExportExcel(List<MS_QR_PickingMain> exportData)
        // {
        //     var sheets = exportData.GroupBy(x => x.iono);
        //     var result = new List<PickingDetailOutExportData>();
        //     foreach (var sheet in sheets)
        //     {
        //         var dataOfSheet = new PickingDetailOutExportData();
        //         dataOfSheet.iono = sheet.Key;
        //         dataOfSheet.listData = new List<MS_QR_PickingDetailDto>();
        //         var data = sheet.Select(data => data).ToList();
        //         foreach (var item in data)
        //         {
        //             var pred = PredicateBuilder.New<MS_QR_PickingDetail>(true);

        //             pred.And(x => x.manuf == item.manuf
        //             && x.manno == item.manno
        //             && x.purno == item.purno
        //             && x.size == item.size);

        //             var pickingDetails = _repositoryAccessor.MS_QR_PickingDetail.FindAll(pred);
        //             var pickingMain = _repositoryAccessor.MS_QR_PickingMain.FindAll(x => x.status == "Release" || x.status == "StorageOut");

        //             var listDetail = await pickingDetails.GroupJoin(
        //                 pickingMain,
        //                 x => new { x.iono, x.manno, x.purno, x.size },
        //                 y => new { y.iono, y.manno, y.purno, y.size },
        //                 (x, y) => new { T1 = x, T2 = y }
        //             ).SelectMany(x => x.T2.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = y })
        //             .Select(x => new MS_QR_PickingDetailDto
        //             {
        //                 manuf = x.T1.manuf,
        //                 Sdat = x.T1.Sdat,
        //                 StorageOutDate = x.T2.StorageOutDate,
        //                 DeclarationNo = x.T2.declaration_no,
        //                 Invno = x.T2.invno,
        //                 Grade = x.T1.Grade,
        //                 QRCodeID = x.T1.QRCodeID,
        //                 manno = x.T1.manno,
        //                 purno = x.T1.purno,
        //                 serial = x.T1.serial,
        //                 size = x.T1.size,
        //                 qty = x.T1.qty,
        //                 crusr = x.T1.crusr,
        //                 crday = x.T1.crday,
        //                 wkshno = x.T1.wkshno,
        //                 prtno = x.T1.prtno,
        //                 wkshqty = x.T1.wkshqty,
        //                 mdat = x.T1.mdat,
        //                 empno = x.T1.empno,
        //                 bitnbr = x.T1.bitnbr,
        //                 ritnbr = x.T1.ritnbr
        //             }).AsNoTracking().ToListAsync();
        //             dataOfSheet.listData.AddRange(listDetail);
        //         }
        //         result.Add(dataOfSheet);
        //     }
        //     return result;
        // }
        public async Task<List<PickingDetailOutExportData>> ExportExcel(List<MS_QR_PickingMain> exportData)
        {
            var sheets = exportData.GroupBy(x => x.iono);
            var result = new List<PickingDetailOutExportData>();

            // Tìm nạp trước tất cả chi tiết cần thiết trong một truy vấn duy nhất nếu có thể
            var allPickingDetails = _repositoryAccessor.MS_QR_PickingDetail.FindAll();
            var allPickingMains = await _repositoryAccessor.MS_QR_PickingMain
                .FindAll(x => x.status == "Release" || x.status == "StorageOut")
                .ToListAsync();

            foreach (var sheet in sheets)
            {
                var dataOfSheet = new PickingDetailOutExportData
                {
                    iono = sheet.Key,
                    listData = new List<MS_QR_PickingDetailDto>()
                };

                foreach (var item in sheet)
                {
                    var filteredDetails = allPickingDetails
                        .Where(x => x.manuf == item.manuf && x.manno == item.manno && x.purno == item.purno && x.size == item.size)
                        .ToList();

                    var listDetail = filteredDetails
                        .GroupJoin(allPickingMains,
                            detail => new { detail.iono, detail.manno, detail.purno, detail.size },
                            main => new { main.iono, main.manno, main.purno, main.size },
                            (detail, mains) => new { Detail = detail, Mains = mains })
                        .SelectMany(x => x.Mains.DefaultIfEmpty(), (x, y) => new { details = x.Detail, mains = y }).
                        Select(x => new MS_QR_PickingDetailDto
                        {
                            manuf = x.details.manuf,
                            Sdat = x.details.Sdat,
                            StorageOutDate = x.mains.StorageOutDate,
                            DeclarationNo = x.mains.declaration_no,
                            Invno = x.mains.invno,
                            Grade = x.details.Grade,
                            QRCodeID = x.details.QRCodeID,
                            manno = x.details.manno,
                            purno = x.details.purno,
                            serial = x.details.serial,
                            size = x.details.size,
                            qty = x.details.qty,
                            crusr = x.details.crusr,
                            crday = x.details.crday,
                            wkshno = x.details.wkshno,
                            prtno = x.details.prtno,
                            wkshqty = x.details.wkshqty,
                            mdat = x.details.mdat,
                            empno = x.details.empno,
                            bitnbr = x.details.bitnbr,
                            ritnbr = x.details.ritnbr
                        })
                        .ToList();

                    dataOfSheet.listData.AddRange(listDetail);
                }

                result.Add(dataOfSheet);
            }

            return result;
        }

    }
}