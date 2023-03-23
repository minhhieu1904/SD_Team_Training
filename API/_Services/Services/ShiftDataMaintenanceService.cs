
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using LinqKit;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class ShiftDataMaintenanceService : IShiftDataMaintenanceService
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public ShiftDataMaintenanceService(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }

        public async Task<OperationResult> Add(MS_Shift shift)
        {
            // phải kiểm tra cái shift đó có tồn tại chưa nếu có thì báo lỗi
            var data = await _repoAccessor.MS_Shift.FindSingle(x => x.Shift == shift.Shift);
            if(data != null) {
                return  new OperationResult(false, "đã tồn tại");
            }
            else {
                // truyền param shift vào models
                var item = Mapper.Map(shift).ToANew<MS_Shift>(x => x.MapEntityKeys());
                _repoAccessor.MS_Shift.Add(item);
            }
            // lưu thông tin
            await _repoAccessor.Save();

            return  new OperationResult(true, "add successfully");
        }

        public async Task<PaginationUtility<MS_Shift>> LoadData(PaginationParam paginationParams, string shift, string shiftName)
        {
            var pred = PredicateBuilder.New<MS_Shift>(true);
            // search theo shift
            if (!string.IsNullOrEmpty(shift))
            {
                pred.And(x => x.Shift == shift.Trim());
            }
            // search theo shiftName
            if (!string.IsNullOrEmpty(shiftName))
            {
                pred.And(x => x.ShiftName == shiftName.Trim());
            }               
            var data = _repoAccessor.MS_Shift.FindAll(pred);
            return  await PaginationUtility<MS_Shift>.CreateAsync(data, paginationParams.PageNumber, paginationParams.PageSize, true);
        }

        public async Task<OperationResult> Update(MS_Shift shift)
        {
            var item = await _repoAccessor.MS_Shift.FirstOrDefaultAsync(x=>x.Manuf.Trim() == "N" && x.Shift == shift.Shift);
            if (item == null)
            {
                return  new OperationResult(false);
            }
            else {
                item.ShiftName = shift.ShiftName.Trim();
                _repoAccessor.MS_Shift.Update(item);
                await _repoAccessor.Save();
            }
            return  new OperationResult(true, "update successfully");
        }
    }
}