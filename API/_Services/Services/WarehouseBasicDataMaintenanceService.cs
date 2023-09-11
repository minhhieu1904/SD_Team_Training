using API._Repositories;
using API._Services.Interfaces;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;
namespace API._Services.Services
{
    public class WarehouseBasicDataMaintenanceService : IWarehouseBasicDataMaintenanceService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public WarehouseBasicDataMaintenanceService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Create(MS_Warehouse model)
        {
            var item = await _repositoryAccessor.MS_Warehouse.FindAll(x => x.WarehouseID == model.WarehouseID && x.Warehouse == "W").FirstOrDefaultAsync();

            if (item != null)
            {
                return new OperationResult(false, "Đã có dữ liệu");
            }

            var modelNew = new MS_Warehouse();

            modelNew.Warehouse = "W";
            modelNew.WarehouseID = model.WarehouseID.Trim();
            modelNew.WarehouseName = model.WarehouseName.Trim();

            _repositoryAccessor.MS_Warehouse.Add(modelNew);

            if (await _repositoryAccessor.Save())
            {
                return new OperationResult(true, "Thêm dữ liệu thành công");
            }

            return new OperationResult(false, "Lỗi APi");

        }
        public async Task<PaginationUtility<MS_Warehouse>> GetAll(PaginationParam pagination, WarehouseBasicDataMaintenanceParam param)
        {
            var pred = PredicateBuilder.New<MS_Warehouse>(true);
            if (!string.IsNullOrEmpty(param.Warehouse))
            {
                pred.And(x => x.Warehouse.Trim() == param.Warehouse.Trim());
            }
            if (!string.IsNullOrEmpty(param.WarehouseID))
            {
                pred.And(x => x.WarehouseID.Trim() == param.WarehouseID.Trim());
            }
            if (!string.IsNullOrEmpty(param.WarehouseName))
            {
                pred.And(x => x.WarehouseName.Trim() == param.WarehouseName.Trim());
            }

            var data = _repositoryAccessor.MS_Warehouse.FindAll(pred).Select(x => new MS_Warehouse()
            {
                Warehouse = "W",
                WarehouseID = x.WarehouseID.Trim(),
                WarehouseName = x.WarehouseName.Trim()
            }).AsNoTracking();

            return await PaginationUtility<MS_Warehouse>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<PaginationUtility<MS_Warehouse>> Search(PaginationParam param, string text)
        {
            var pred = PredicateBuilder.New<MS_Warehouse>();

            if (!string.IsNullOrEmpty(text))
            {
                text = text.ToLower();
                pred = pred.And(x => x.Warehouse.Contains(text) || x.WarehouseName.Contains(text));
            }

            var data = _repositoryAccessor.MS_Warehouse.FindAll(pred).Select(x => new MS_Warehouse()
            {
                Warehouse = "W",
                WarehouseID = x.WarehouseID.Trim(),
                WarehouseName = x.WarehouseName.Trim()
                
            }).AsNoTracking();

            return await PaginationUtility<MS_Warehouse>.CreateAsync(data, param.PageNumber, param.PageSize);
        }

        public async Task<OperationResult> Update(MS_Warehouse model)
        {
            var item = await _repositoryAccessor.MS_Warehouse.FirstOrDefaultAsync(x => x.WarehouseID.Trim() == model.WarehouseID.Trim());

            if (item == null)
            {
                return new OperationResult(false, "Chưa có dữ liệu");
            }
            else
            {
                item.Warehouse = "W";
                item.WarehouseName = model.WarehouseName;

                _repositoryAccessor.MS_Warehouse.Update(item);

                await _repositoryAccessor.Save();
            }
            return new OperationResult(true, "Cập nhật dữ liệu thành công");
        }
    }
}