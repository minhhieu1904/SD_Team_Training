
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.DepartmentDataMaintenance
{
    public class S_DepartmentDataMaintenance : I_DepartmentDataMaintenance
    {

        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _reposioryAccessor;
        // Thêm vào hàm khởi tạo
        public S_DepartmentDataMaintenance(IRepositoryAccessor reposioryAccessor)
        {
            _reposioryAccessor = reposioryAccessor;
        }
        public async Task<OperationResult> Addnew(MS_Department model)
        {
           var originalItem = await _reposioryAccessor.MS_Department.FindAll(x => x.Manuf == model.Manuf 
            && x.ParNo == model.ParNo.Trim()).FirstOrDefaultAsync();
            if(originalItem != null){
                return new OperationResult(false);
            }else{
                model.Manuf= "N" ;
                _reposioryAccessor.MS_Department.Add(model);
                if(await _reposioryAccessor.Save())
                {
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }
        }

        public async Task<PaginationUtility<MS_Department>> GetData(PaginationParam pagination, DepartmentDataMaintenanceParam param)
        {
            var pred_MS_Location = PredicateBuilder.New<MS_Department>(true);
            if(!string.IsNullOrEmpty(param.ParNo))
                pred_MS_Location.And(x=>x.ParNo == param.ParNo);
            if(!string.IsNullOrEmpty(param.ParName))
                pred_MS_Location.And(x=>x.ParName == param.ParName);
            
            var data = _reposioryAccessor.MS_Department.FindAll(pred_MS_Location);

            
            return await PaginationUtility<MS_Department>.CreateAsync(data,pagination.PageNumber,pagination.PageSize);
        }

        public async Task<MS_Department> GetDataOnly(string manuf, string parNo)
        {
            return await _reposioryAccessor.MS_Department.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.ParNo == parNo);

        }

        public async Task<OperationResult> Update(MS_Department model)
        {
            var originalItem = await _reposioryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.ParNo.Trim() == model.ParNo.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            originalItem.ParName = model.ParName.Trim();

            _reposioryAccessor.MS_Department.Update(originalItem);

            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }
    }
}