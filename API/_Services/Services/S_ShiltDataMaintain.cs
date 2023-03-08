using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class S_ShiltDataMaintain : I_ShiltDataMaintain
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _reposioryAccessor;
        // Thêm vào hàm khởi tạo
        public S_ShiltDataMaintain(IRepositoryAccessor reposioryAccessor)
        {
            _reposioryAccessor = reposioryAccessor;
        }

        public async Task<List<MS_Shift>> GetData()
        {
            return await _reposioryAccessor.MS_Shift.FindAll().ToListAsync();
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