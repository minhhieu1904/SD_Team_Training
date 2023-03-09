using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class SMMShiftServices : IMSShiftServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public SMMShiftServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> AddNewShift(MS_Shift model)
        {
            // 1. Kiểm tra shift đã tồn tại trong db chưa ?
            var originalItem = await _repositoryAccessor.MSShift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim());
            // 2. Nếu có rồi, thì không được phép thêm nữa
            if (originalItem != null)
                return new OperationResult(false);
            // 3. nếu chưa có thì thêm mới, gán cứng N
            model.Manuf = "N";
            // 4. Thêm mới vào db
            _repositoryAccessor.MSShift.Add(model);
            // Lưu lại và trả về kq
            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }
        
        public async Task<List<MS_Shift>> GetAllShift()
        {
            var data = await _repositoryAccessor.MSShift.FindAll().ToListAsync();
            return data;
        }

        public async Task<PaginationUtility<MS_Shift>> GetDataPagination(PaginationParam param, string shift, string shiftName)
        {
            // Lấy hết  danh sách MS shift
            var data = await _repositoryAccessor.MSShift.FindAll().ToListAsync();
            // Nếu có shift != rỗng hoặc shift != null thì lấy danh sách theo đk shift
            // if(shift != null || shift != string.Empty) // c1
            if(!string.IsNullOrEmpty(shift))
                data = data.Where(item => item.Shift == shift).ToList();
            // Nếu có shiftName != rỗng hoặc shiftName != null thì lấy danh sách theo đk shift
            if(!string.IsNullOrEmpty(shiftName))
                data = data.Where(item => item.ShiftName.Contains(shiftName)).ToList();

            return PaginationUtility<MS_Shift>.Create(data, param.PageNumber,param.PageSize);
        }

        public async Task<OperationResult> UpdateShift(MS_Shift model)
        {
            // 1. tìm item cần sửa với điều kiện là các khóa chính (kiểm tra ở cột column trong DB)
            var msShift = await _repositoryAccessor.MSShift.FirstOrDefaultAsync(item => item.Manuf == model.Manuf && item.Shift == model.Shift);
            // 2. Nếu msShift không có , trả  về null
            if (msShift == null)
                return new OperationResult(false);
            // Ngược lại , msShift có thì sửa 

            // gán data chỉnh sửa 
            msShift.ShiftName = model.ShiftName;

            // 3.Lưu lại 
            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<OperationResult> Delete(MS_Shift model)
        {
            // 1. Kiểm tra shift đã tồn tại trong db chưa ?
            var msShift = await _repositoryAccessor.MSShift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == model.Shift.Trim());
            if (msShift == null)
            {
                return new OperationResult(false);
            }
            msShift.Manuf = model.Manuf;
            msShift.Shift = model.Shift;
            msShift.ShiftName = model.ShiftName;
            _repositoryAccessor.MSShift.Remove(msShift);
            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }
    }
}