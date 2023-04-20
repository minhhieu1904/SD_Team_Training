using SD3_API.Helpers.Utilities;
using API.Models;

using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.ShiftDataMaintain;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Maintain
{
    public class S_ShiftDataMaintainServices : I_ShiftDataMaintainServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_ShiftDataMaintainServices(IRepositoryAccessor reposioryAccessor)
        {
            _repositoryAccessor = reposioryAccessor;
        }

        #region Add
        public async Task<OperationResult> Add(MsShift model)
        {
            // Tìm xem giá trị đã tồn tại trong dữ liệu chưa
            var originalItem = await _repositoryAccessor.MS_Shift.FindAll(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim()).FirstOrDefaultAsync();
            if (originalItem != null)
                return new OperationResult(false);

            // Nếu giá trị chưa tồn tại thì thêm vào dữ liệu
            model.Manuf = "N";
            _repositoryAccessor.MS_Shift.Add(model);

            // Kiểm tra xem đã lưu giá trị thành công chưa
            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false);
            }
        }
        #endregion

        #region Update
        public async Task<OperationResult> Update(MsShift model)
        {
            var originalItem = await _repositoryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            originalItem.ShiftName = model.ShiftName.Trim();
            _repositoryAccessor.MS_Shift.Update(originalItem);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false);
            }
        }
        #endregion

        #region Delete
        public async Task<OperationResult> Delete(string shift)
        {
            if (string.IsNullOrEmpty(shift.Trim()))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == shift.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            _repositoryAccessor.MS_Shift.Remove(originalItem);
            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false);
            }
        }
        #endregion

        #region GetData
        public async Task<PaginationUtility<MsShift>> GetData(PaginationParam pagination, ShiftDataMaintainParam param)
        {
            var pred_MS_Shift = PredicateBuilder.New<MsShift>(true);
            if (!string.IsNullOrEmpty(param.Shift))
                pred_MS_Shift.And(x => x.Shift.Trim().ToLower().Contains(param.Shift.Trim().ToLower()));
            if (!string.IsNullOrEmpty(param.Shift_Name))
                pred_MS_Shift.And(x => x.ShiftName.Trim().ToLower().Contains(param.Shift_Name.Trim().ToLower()));

            var data = _repositoryAccessor.MS_Shift.FindAll(pred_MS_Shift);
            return await PaginationUtility<MsShift>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsShift> GetDataOnly(string manuf, string shift)
        {
            return await _repositoryAccessor.MS_Shift.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.Shift == shift);
        }
        #endregion
    }
}