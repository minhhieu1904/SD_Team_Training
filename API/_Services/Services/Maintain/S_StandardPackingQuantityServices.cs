using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.StandardPackingQuantity;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Maintain
{
    public class S_StandardPackingQuantityServices : I_StandardPackingQuantityServices
    {
        
        public readonly IRepositoryAccessor _repositoryAccessor;
        public S_StandardPackingQuantityServices(IRepositoryAccessor repositoryAccessor){
            _repositoryAccessor = repositoryAccessor;
        }
        public async Task<PaginationUtility<MsPackage>> GetData(PaginationParam pagination, StandardPackingQuantityParam param)
        {
            var pred_MS_Package = PredicateBuilder.New<MsPackage>(true);
            if(!string.IsNullOrEmpty(param.packageNo))
                pred_MS_Package.And(x => x.PackageNo == param.packageNo);
            if(param.packageQty != 0)
                pred_MS_Package.And(x => x.PackageQty == param.packageQty);

            var data = await _repositoryAccessor.Ms_Package.FindAll(pred_MS_Package).ToListAsync();
            
            return PaginationUtility<MsPackage>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsPackage> GetDataOnly(string manuf, string packageNo)
        {
            return await _repositoryAccessor.Ms_Package.FirstOrDefaultAsync(
                item => item.Manuf == manuf.Trim() && item.PackageNo == packageNo.Trim());
        }
        public async Task<OperationResult> Add(MsPackage model)
        {
            var original = await _repositoryAccessor.Ms_Package.FindAll(
                x=>x.Manuf == model.Manuf 
                && x.PackageNo == model.PackageNo.Trim()).FirstOrDefaultAsync();
            if (original != null)
                return new OperationResult(false);
            else
                model.Manuf = "N";
                _repositoryAccessor.Ms_Package.Add(model);
                if(await _repositoryAccessor.Save())
                    return new OperationResult(true);
                return new OperationResult(false);
        }

        public async Task<OperationResult> Update(MsPackage model)
        {
            var originalItem = this._repositoryAccessor.Ms_Package.FirstOrDefault(x => x.Manuf == "N"
            && x.PackageNo == model.PackageNo.Trim());

            if(originalItem == null)
                return new OperationResult(false);
            
            originalItem.PackageQty = model.PackageQty;
            _repositoryAccessor.Ms_Package.Update(originalItem);

            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public Task<OperationResult> Delete(string packageNo)
        {
            throw new NotImplementedException();
        }


        
    }
}