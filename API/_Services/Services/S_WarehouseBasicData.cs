
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using SD3_API.Helpers.Utilities;
using Microsoft.EntityFrameworkCore;
using System;

namespace API._Services.Services
{
    public class S_WarehouseBasicData : I_WarehouseBasicData
    {
        private readonly IRepositoryAccessor _reposioryAccessor;

        public S_WarehouseBasicData(IRepositoryAccessor repositoryAccessor)
        {
            _reposioryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> addNew(MS_Location model)
        {

            var originalItem = await _reposioryAccessor.MS_Location.FindAll(x=> x.Manuf.Trim() == model.Manuf.Trim() &&
            x.StoreH.Trim() == model.StoreH.Trim()).FirstOrDefaultAsync();
            if(originalItem != null)
                return new OperationResult(false);
            else{
                model.Manuf = "N";
                _reposioryAccessor.MS_Location.Add(model);
                if(await _reposioryAccessor.Save()){
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }
            
        }

        public async Task<PaginationUtility<MS_Location>> getData(PaginationParam pagination, WarehouseBasicData param)
        {
            var pred_MS_Location = PredicateBuilder.New<MS_Location>(true);
            if (!string.IsNullOrEmpty(param.StoreH))
                pred_MS_Location.And(x => x.StoreH == param.StoreH);
                
            if (!string.IsNullOrEmpty(param.LocationName))
                pred_MS_Location.And(x => x.LocationName == param.LocationName);
            var data = await _reposioryAccessor.MS_Location.FindAll(pred_MS_Location).ToListAsync();

            return PaginationUtility<MS_Location>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MS_Location> getDataOnly(string manuf, string StoreH)
        {
            return await _reposioryAccessor.MS_Location.FirstOrDefaultAsync(x => x.Manuf.ToUpper() == manuf.ToUpper() && x.StoreH.ToUpper() == StoreH.ToUpper());
        }

        public async Task<OperationResult> Update(MS_Location model)
        {
            var originalItem = await _reposioryAccessor.MS_Location.FirstOrDefaultAsync(
                x=> x.Manuf == "N" && x.StoreH.ToUpper().Trim() == model.StoreH.ToUpper().Trim()
            );
            if(originalItem == null)
                return new OperationResult(false);

            originalItem.LocationName = model.LocationName.Trim();
            _reposioryAccessor.MS_Location.Update(originalItem);
            if(await _reposioryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }
    }
}