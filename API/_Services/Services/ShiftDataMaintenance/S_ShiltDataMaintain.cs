using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.ShiltDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class S_ShiltDataMaintain : I_ShiftDataMaintain
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _reposioryAccessor;
        // Thêm vào hàm khởi tạo
        public S_ShiltDataMaintain(IRepositoryAccessor reposioryAccessor)
        {
            _reposioryAccessor = reposioryAccessor;
        }

        public async Task<PaginationUtility<MS_Shift>> GetData(PaginationParam pagination ,ShiftDataMaintainParam param)
        {
            var pred_MS_Shift = PredicateBuilder.New<MS_Shift>(true);
            if(!string.IsNullOrEmpty(param.Shift)) { 
                pred_MS_Shift.And(x => x.Shift == param.Shift);
            }
            if(!string.IsNullOrEmpty(param.Shift_Name)) { 
                pred_MS_Shift.And(x => x.ShiftName == param.Shift_Name);
            }
            var data = _reposioryAccessor.MS_Shift.FindAll(pred_MS_Shift);
            return await PaginationUtility<MS_Shift>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }
        public async Task<OperationResult> Addnew(MS_Shift model)
        {
            var originalItem = await _reposioryAccessor.MS_Shift.FindAll(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim()).FirstOrDefaultAsync();
            if (originalItem != null)
            {
                return new OperationResult(false);
            }
            else
            {
                model.Manuf = "N";
                _reposioryAccessor.MS_Shift.Add(model);
                if (await _reposioryAccessor.Save())
                {
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }

        }


        // chạy đc

        public async Task<OperationResult> Update(MS_Shift model)
        {
            var originalItem = await _reposioryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            originalItem.ShiftName = model.ShiftName.Trim();

            _reposioryAccessor.MS_Shift.Update(originalItem);

            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }

        public async Task<MS_Shift> GetDataOnly(string manuf, string shift)
        {
            return await _reposioryAccessor.MS_Shift.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.Shift == shift);
        }

        public async Task<OperationResult> Delete(MS_Shift model)
        {

            var originalItem = await _reposioryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim());
            if (originalItem == null)
                return new OperationResult(false);
            // 
            originalItem.Manuf = model.Manuf;
            originalItem.Shift = model.Shift;
            originalItem.ShiftName = model.ShiftName;

            _reposioryAccessor.MS_Shift.Remove(originalItem);
            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }


    }
}