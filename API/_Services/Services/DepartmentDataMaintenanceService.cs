
using API._Repositories;
using API._Services.Interfaces;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;


namespace API._Services.Services
{
    public class DepartmentDataMaintenanceService : IDepartmentDataMaintenanceService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public DepartmentDataMaintenanceService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Create(MS_Department model)
        {
            var item = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.ParNo.Trim() == model.ParNo.Trim());

            if (item != null)
            {
                return new OperationResult(false, "Đã có dữ liệu");
            }

            var modelNew = new MS_Department();

            modelNew.Manuf = "N";
            modelNew.ParNo = model.ParNo;
            modelNew.ParName = model.ParName;

            _repositoryAccessor.MS_Department.Add(modelNew);
            await _repositoryAccessor.Save();
            return new OperationResult(true, "Đã thêm dữ liệu thành công");

        }

        public async Task<PaginationUtility<MS_Department>> GetAll(PaginationParam pagination, DepartmentDataMaintenanceParam param)
        {
            var pre = PredicateBuilder.New<MS_Department>(true);

            if (!string.IsNullOrEmpty(param.Manuf))
            {
                pre.And(x => x.Manuf == "N");
            }
            if (!string.IsNullOrEmpty(param.ParNo))
            {
                pre.And(x => x.ParNo.Trim() == param.ParNo.Trim());
            }
            if (!string.IsNullOrEmpty(param.ParName))
            {
                pre.And(x => x.ParName.Trim() == param.ParName.Trim());
            }

            var data = _repositoryAccessor.MS_Department.FindAll(pre).AsNoTracking();

            return await PaginationUtility<MS_Department>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<PaginationUtility<MS_Department>> Search(PaginationParam pagination, string text)
        {
            var pre = PredicateBuilder.New<MS_Department>();

            if (!string.IsNullOrEmpty(text))
            {
                text = text.ToLower();
                pre = pre.And(x => x.ParName.Contains(text) || x.ParNo.Contains(text));
            }

            var data = _repositoryAccessor.MS_Department.FindAll(pre).AsNoTracking();

            return await PaginationUtility<MS_Department>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<OperationResult> Update(MS_Department model)
        {
            var item = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.ParNo.Trim() == model.ParNo.Trim());

            if (item == null)
            {
                return new OperationResult(false, "Chưa có dữ liệu");
            }

            item.ParName = model.ParName.Trim();

            _repositoryAccessor.MS_Department.Update(item);
            await _repositoryAccessor.Save();

            return new OperationResult(true, "Đã cập nhật thành công");
        }
    }
}