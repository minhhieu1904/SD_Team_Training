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
    public class SMSLocationServices : IMSLocationServices
    {   
        private readonly IRepositoryAccessor _repositoryAccessor;
        public SMSLocationServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> AddNew(MS_Location model)
        {
            // 1. Kiểm tra Location đã tồn tại trong db chưa ?
            var originalItem = await _repositoryAccessor.MSLocation.FirstOrDefaultAsync( x=> x.Manuf.Trim() == "N" && x.StoreH.Trim() == model.StoreH.Trim());
            // 2. Nếu có rồi, thì không được phép thêm nữa
            if(originalItem != null)
            return new OperationResult(false);                   
            // 3. nếu chưa có thì thêm mới, gán cứng N
            model.Manuf = "N";
            // 4. Thêm mới vào db
            _repositoryAccessor.MSLocation.Add(model);
            // Lưu lại và trả về kq
            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }
        
        public async Task<List<MS_Location>> GetAllData()
        {
            var data = await _repositoryAccessor.MSLocation.FindAll().ToListAsync();
            return data;
        }

        public async Task<PaginationUtility<MS_Location>> GetDataPagination(PaginationParam param, string storeH, string locationName)
        {
            // Lấy hết  danh sách MS Location
            var data = await _repositoryAccessor.MSLocation.FindAll().ToListAsync(); 
            // Nếu có shift != rỗng hoặc shift != null thì lấy danh sách theo đk location
            if(!string.IsNullOrEmpty(storeH))
                 data = data.Where(x => x.StoreH == storeH).ToList();
            // Nếu có shiftName != rỗng hoặc shiftName != null thì lấy danh sách theo đk location
            if(!string.IsNullOrEmpty(locationName))
                data = data.Where(x => x.LocationName == locationName).ToList();
            return PaginationUtility<MS_Location>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<MS_Location> GetItem(string manuf, string storeH)
        {
            var data = await _repositoryAccessor.MSLocation.FirstOrDefaultAsync(x => x.Manuf == manuf && x.StoreH == storeH);
            return data;
        }

        public async Task<OperationResult> UpdateLocation(MS_Location model)
        {
            // 1. tìm item cần sửa với điều kiện là các khóa chính (kiểm tra ở cột column trong DB)
                var originalItem = await _repositoryAccessor.MSLocation.FirstOrDefaultAsync(x => x.Manuf == model.Manuf && x.StoreH == model.StoreH);
            // 2. Nếu msLocation không có , trả  về null
                if(originalItem == null)
                return new OperationResult(false);
            // Ngược lại , msLocation có thì sửa 
                originalItem.LocationName = model.LocationName;
            // gán data chỉnh sửa
            // 3.Lưu lại  
                if(await _repositoryAccessor.Save())
                    return new OperationResult(true);
                return new OperationResult(false);

        }
    }
}