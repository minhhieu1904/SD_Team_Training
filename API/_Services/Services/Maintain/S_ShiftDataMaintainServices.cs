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

        private readonly IRepositoryAccessor _reposioryAccessor;

        public S_ShiftDataMaintainServices (IRepositoryAccessor reposioryAccessor){
            _reposioryAccessor = reposioryAccessor;
        }

        public async Task<OperationResult> Addnew(MsShift model)
        {
            var originalItem = await _reposioryAccessor.MS_Shift.FindAll(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim()).FirstOrDefaultAsync();
            if(originalItem != null){
                return new OperationResult(false);
            } else{
                model.Manuf = "N";
                _reposioryAccessor.MS_Shift.Add(model);
                if(await _reposioryAccessor.Save()){
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }
        }

        public async Task<OperationResult> Update(MsShift model)
        {
            var originalItem = await _reposioryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim());

            if(originalItem == null)
                return new OperationResult(false);
            
            originalItem.ShiftName = model.ShiftName.Trim();
            _reposioryAccessor.MS_Shift.Update(originalItem);

            if(await _reposioryAccessor.Save()){
                return new OperationResult(true);
            }

            return new OperationResult(false);

        }

        public async Task<OperationResult> Delete(string shift)
        {
            var originalItem = await _reposioryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == shift.Trim());

            if(originalItem == null)
                return new OperationResult(false);

            _reposioryAccessor.MS_Shift.Remove(originalItem);
            if(await _reposioryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<PaginationUtility<MsShift>> GetData(PaginationParam pagination, ShiftDataMaintainParam param)
        {
            var pred_MS_Shift = PredicateBuilder.New<MsShift>(true);
            if(!string.IsNullOrEmpty(param.Shift)){
                pred_MS_Shift.And(x => x.Shift == param.Shift);
            }

            if(!string.IsNullOrEmpty(param.Shift_Name)){
                pred_MS_Shift.And(x => x.ShiftName == param.Shift_Name);
            }

            var data = _reposioryAccessor.MS_Shift.FindAll(pred_MS_Shift);
            return await PaginationUtility<MsShift>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsShift> GetDataOnly(string manuf, string shift)
        {
            return await _reposioryAccessor.MS_Shift.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.Shift == shift);
        }

        
    }
}