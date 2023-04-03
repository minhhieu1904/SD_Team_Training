using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.ShiftDataMaintain;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class ShiftDataMaintainService : IShiftDataMaintainService
    {
        private readonly IRepositoryAccessor _repoAcceesor;
        public ShiftDataMaintainService(IRepositoryAccessor repoAcceesor)
        {
            _repoAcceesor = repoAcceesor;
        }
        public async Task<OperationResult> Create(MS_Shift msshifts)
        {
            var item = await _repoAcceesor.MS_Shift.FindAll(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == msshifts.Shift.Trim()).FirstOrDefaultAsync();



            if (item != null)
            {
                return new OperationResult(false, "Đã có dữ liệu");
            }
            else
            {
                var model = new MS_Shift();

                model.Manuf = "N";
                model.Shift = msshifts.Shift;
                model.ShiftName = msshifts.ShiftName;

                _repoAcceesor.MS_Shift.Add(model);

                if (await _repoAcceesor.Save())
                {
                    return new OperationResult(true, "Thêm dữ liệu thành công");
                }

                return new OperationResult(false);
            }

        }

        public async Task<PaginationUtility<MS_Shift>> GetAll(PaginationParam pagination, ShiftDataMaintainParam param)
        {
            var pred = PredicateBuilder.New<MS_Shift>(true);

            if (!string.IsNullOrEmpty(param.Shift))
                pred.And(x => x.Shift.Trim() == param.Shift.Trim());
            if (!string.IsNullOrEmpty(param.ShiftName))
                pred.And(x => x.ShiftName.Trim() == param.ShiftName.Trim());

            var data = _repoAcceesor.MS_Shift.FindAll(pred).AsNoTracking();
            return await PaginationUtility<MS_Shift>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MS_Shift> GetItem(string manuf, string shift)
        {
            var item = await _repoAcceesor.MS_Shift.FindAll(x => x.Manuf.Trim() == "N" && x.Shift == shift.Trim()).FirstOrDefaultAsync();

            return item;
        }

        public async Task<OperationResult> Update(MS_Shift msshift)
        {
            var item = await _repoAcceesor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == msshift.Shift);

            if (item == null)
            {
                return new OperationResult(false, "Chưa có dữ liệu thành công");
            }
            else
            {
                item.ShiftName = msshift.ShiftName.Trim();

                _repoAcceesor.MS_Shift.Update(item);

                await _repoAcceesor.Save();
            }

            return new OperationResult(true, "Cập nhật dữ liệu thành công");
        }
    }
}