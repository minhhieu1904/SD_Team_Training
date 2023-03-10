
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.WarehouseBasicData;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.WarehouseBasicDataMaintenance
{
    public class S_WarehouseBasicData : I_WarehouseBasicData
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _reposioryAccessor;



        // Thêm vào hàm khởi tạo
        public S_WarehouseBasicData(IRepositoryAccessor reposioryAccessor)
        {
            _reposioryAccessor = reposioryAccessor;
        }
        public async Task<PaginationUtility<MS_Location>> GetData(PaginationParam pagination ,WarehouseBasicDataParam param)
        {
            var pred_MS_Location = PredicateBuilder.New<MS_Location>(true);
            if(!string.IsNullOrEmpty(param.StoreH))
                pred_MS_Location.And(x=>x.StoreH == param.StoreH);
            if(!string.IsNullOrEmpty(param.LocationName))
                pred_MS_Location.And(x=>x.LocationName == param.LocationName);
            
            var data = _reposioryAccessor.MS_Location.FindAll(pred_MS_Location);

            
            return await PaginationUtility<MS_Location>.CreateAsync(data,pagination.PageNumber,pagination.PageSize);
        }
        public async Task<OperationResult> Addnew(MS_Location model)
        {
            var originalItem = await _reposioryAccessor.MS_Location.FindAll(x => x.Manuf == model.Manuf 
            && x.StoreH == model.StoreH.Trim()).FirstOrDefaultAsync();
            if(originalItem != null){
                return new OperationResult(false);
            }else{
                model.Manuf= "N" ;
                _reposioryAccessor.MS_Location.Add(model);
                if(await _reposioryAccessor.Save())
                {
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }

        }


        // chạy đc

        public async Task<OperationResult> Update(MS_Location model)
        {
            var originalItem = await _reposioryAccessor.MS_Location.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.StoreH.Trim() == model.StoreH.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            originalItem.LocationName = model.LocationName.Trim();

            _reposioryAccessor.MS_Location.Update(originalItem);

            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }

        public async Task<MS_Location> GetDataOnly(string manuf, string StoreH)
        {
            return await _reposioryAccessor.MS_Location.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.StoreH == StoreH);

        }

        public async Task<OperationResult> Delete(MS_Location model)
        {
             var originalItem = await _reposioryAccessor.MS_Location.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.StoreH.Trim() == model.StoreH.Trim());
            if (originalItem == null)
                return new OperationResult(false);
            // 
            originalItem.Manuf = model.Manuf;
            originalItem.StoreH = model.StoreH;
            originalItem.LocationName = model.LocationName;

            _reposioryAccessor.MS_Location.Remove(originalItem);
            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);   
            
        }


    }
}