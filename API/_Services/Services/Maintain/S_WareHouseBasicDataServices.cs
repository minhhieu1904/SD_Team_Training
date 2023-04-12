using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Maintain.WareHouseBasicData;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Maintain
{
    public class S_WareHouseBasicDataServices : I_WareHouseBasicDataServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        
        public S_WareHouseBasicDataServices (IRepositoryAccessor repositoryAccessor){
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Add(MsLocation model)
        {
            var original = await _repositoryAccessor.Ms_Location.FindAll(
                x => x.Manuf == model.Manuf 
                && x.StoreH == model.StoreH.Trim()).FirstOrDefaultAsync();

                if(original != null){
                    return new OperationResult(false);
                } else {
                    // model.Manuf = 'N';
                    // _repositoryAccessor.MS_Location.Add(model);
                    // if(await _repositoryAccessor.Save())
                    //     return new OperationResult(true);
                    // return new OperationResult(false);
                    model.Manuf = "N";
                    _repositoryAccessor.Ms_Location.Add(model);
                    if(await _repositoryAccessor.Save())
                    return new OperationResult(true);
                    return new OperationResult(false);
                }
        }

        public async Task<PaginationUtility<MsLocation>> GetData(PaginationParam pagination, WareHouseBasicDataParam param)
        {
            var pred = PredicateBuilder.New<MsLocation>(true);
            if(!string.IsNullOrEmpty(param.StoreH)){
                pred.And(x => x.StoreH == param.StoreH);
            }
            if(!string.IsNullOrEmpty(param.LocationName)){
                pred.And(x => x.LocationName == param.LocationName);
            }

            var data = await _repositoryAccessor.Ms_Location.FindAll(pred).ToListAsync();
            return PaginationUtility<MsLocation>
            .Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsLocation> GetDataOnly(string manuf, string storeH)
        {
            return await _repositoryAccessor.Ms_Location.FirstOrDefaultAsync(
                item => item.Manuf.ToUpper() == manuf.ToUpper() 
                && item.StoreH.ToUpper() == storeH.ToUpper());
        }

        public async Task<OperationResult> Update(MsLocation model)
        {
            var original = await _repositoryAccessor.Ms_Location.FirstOrDefaultAsync(
                x => x.Manuf == model.Manuf && x.StoreH == model.StoreH.Trim());
            if(original == null){
                return new OperationResult(false);
            }
            original.LocationName = model.LocationName.Trim();
            _repositoryAccessor.Ms_Location.Update(original);
            if(await _repositoryAccessor.Save()){
                return new OperationResult(true);
            }
            return new OperationResult(false);
    }
}
}