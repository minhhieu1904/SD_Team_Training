using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.transaction;
using API.DTOs.Transaction.ReprintWarehouseScan;
using API.DTOs.Transaction.WarehouseScan;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class ReprintWarehouseScanServices : I_ReprintWarehouseScanService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly string _manuf;
        private readonly IConfiguration _config;

        public ReprintWarehouseScanServices(IRepositoryAccessor repository, IConfiguration config)
        {
            _config = config;
            _repository = repository;
            _manuf = _config["Appsettings: FactoryCode"];
        }

        public async Task<List<WarehouseScanExportDto>> GetDataExport(List<ReprintWarehouseScanDto> dataReprints)
        {
            var data = dataReprints.GroupBy(x => new { x.TrNo, x.ManNo })
                                    .Select(g => new WarehouseScanExportDto
                                    {
                                        Key = g.Key.ManNo,
                                        ListWarehouseScan = g.GroupBy(x => new { x.ManNo, x.PurNo, x.Size })
                                                            .Select(x => new MS_QR_StorageDto
                                                            {
                                                                TrNo = x.First().TrNo,
                                                                SDat = x.First().SDat,
                                                                StoreH = x.First().StoreH,
                                                                ParNo = x.Key.ManNo,
                                                                ManNo = x.Key.ManNo,
                                                                PurNo = x.Key.PurNo,
                                                                Size = x.Key.Size,
                                                                Qty = x.Sum(x => x.StorageQty),
                                                                BitNbr = x.First().BitNbr,
                                                                RModel = x.First().RModel
                                                            }).ToList(),
                                        Qty = (int)Math.Floor(g.Sum(x => x.StorageQty))
                                    }).ToList();
            return await Task.FromResult(data);
        }

        public Task<PaginationUtility<ReprintWarehouseScanDto>> GetDataPagination(ReprintWarehouseScanParam param, PaginationParam pagination)
        {
            var predOrder = PredicateBuilder.New<MS_QR_Order>(true);
            var predStorage = PredicateBuilder.New<MS_QR_Storage>(x => x.trno != null);

            if (!string.IsNullOrWhiteSpace(param.dateFrom) && !string.IsNullOrWhiteSpace(param.dateTo))
            {
                predStorage.And(x => x.Sdat >= Convert.ToDateTime(param.dateFrom).Date && x.Sdat <= Convert.ToDateTime(param.dateTo).Date);
            }

            if (!string.IsNullOrWhiteSpace(param.trno))
                predStorage.And(x => x.trno == param.trno);
            if (!string.IsNullOrWhiteSpace(param.location))
                predStorage.And(x => x.storeh == param.location);
            if (!string.IsNullOrWhiteSpace(param.department))
                predStorage.And(x => x.parno == param.department);
            if (!string.IsNullOrWhiteSpace(param.manno))
                predStorage.And(x => x.manno.Trim() == param.manno.Trim());
            if (!string.IsNullOrWhiteSpace(param.purno))
                predStorage.And(x => x.purno.Trim() == param.purno.Trim());
            if (!string.IsNullOrWhiteSpace(param.size))
                predStorage.And(x => x.size.Trim() == param.size.Trim());

            var storeages = _repository.MS_QR_Storage.FindAll(predStorage);
            var locations = _repository.MS_Location.FindAll();
            var departments = _repository.MS_Department.FindAll();
            var orders = _repository.MS_QR_Order.FindAll();
            var data = storeages.GroupJoin(locations,
            x => new { StoreH = x.storeh },
            y => new { StoreH = y.StoreH },
            (x, y) => new { storage = x, location = y })
            .SelectMany(x => x.location.DefaultIfEmpty(),
            (x, y) => new { x.storage, location = y })
            .GroupJoin(departments,
            x => new { ParNo = x.storage.parno },
            y => new { ParNo = y.ParNo },
            (x, y) => new { x.storage, x.location, department = y })
            .SelectMany(x => x.department.DefaultIfEmpty(),
            (x, y) => new { x.storage, x.location, department = y })
            .GroupJoin(orders,
            x => new { x.storage.manno, x.storage.purno, x.storage.size, x.storage.wkshno, x.storage.prtno },
            y => new { y.manno, y.purno, y.size, y.wkshno, y.prtno },
            (x, y) => new { x.storage, x.location, x.department, order = y })
            .SelectMany(x => x.order.DefaultIfEmpty(),
            (x, y) => new { x.storage, x.location, x.department, order = y })
            .OrderBy(x => x.storage.trno)
            .GroupBy(x => new
            {
                x.storage.Sdat,
                x.storage.trno,
                x.location.LocationName,
                x.department.ParName,
                x.storage.manno,
                x.storage.purno,
                x.storage.size
            }).Select(x => new ReprintWarehouseScanDto()
            {
                SDat = x.Key.Sdat,
                TrNo = x.Key.trno,
                StorageQty = x.Sum(y => y.storage.qty),
                Location = x.Key.LocationName,
                Department = x.Key.ParName,
                ManNo = x.Key.manno,
                PurNo = x.Key.purno,
                Size = x.Key.size,
                StoreH = x.Max(y => y.storage.storeh),
                ParNo = x.Max(y => y.storage.parno),
                BitNbr = x.Max(y => y.order.bitnbr),
                RModel = x.Max(y => y.order.rmodel)
            }).AsNoTracking();
            return PaginationUtility<ReprintWarehouseScanDto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }
    }
}