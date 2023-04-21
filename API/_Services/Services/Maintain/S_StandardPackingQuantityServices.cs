using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Maintain
{
    public class S_StandardPackingQuantityServices : I_StandardPackingQuantityServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_StandardPackingQuantityServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        #region Add
        public async Task<OperationResult> Add(StandardPackingQuantityParam model)
        {
            var originalItem = this._repositoryAccessor.MS_Package.FindAll(x => x.Manuf == model.Manuf
            && x.PackageNo == model.PackageNo.Trim());

            if (originalItem == null)
                return new OperationResult { IsSuccess = false, Error = "Data is exists" };

            var item = Mapper.Map(model).ToANew<MsPackage>(x => x.MapEntityKeys());
            _repositoryAccessor.MS_Package.Add(item);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult { IsSuccess = true };
            }
            catch
            {
                return new OperationResult { IsSuccess = false, Error = "" };
            }
        }
        #endregion

        #region Update
        public async Task<OperationResult> Update(StandardPackingQuantityParam model)
        {
            var originalItem = this._repositoryAccessor.MS_Package.FirstOrDefault(x => x.Manuf == model.Manuf
            && x.PackageNo == model.PackageNo.Trim());

            if (originalItem == null)
                return new OperationResult { IsSuccess = false, Error = "Data Not Found" };

            originalItem.PackageQty = model.PackageQty;
            _repositoryAccessor.MS_Package.Update(originalItem);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult { IsSuccess = true };
            }
            catch
            {
                return new OperationResult { IsSuccess = false, Error = "" };
            }
        }
        #endregion

        #region Delete
        public async Task<OperationResult> Delete(string packageNo)
        {
            if (!string.IsNullOrEmpty(packageNo))
                 return new OperationResult { IsSuccess = false };

            var originalItem = await _repositoryAccessor.MS_Package.FirstOrDefaultAsync(x => x.Manuf == "N" && x.PackageNo == packageNo.Trim());

            if (originalItem == null)
                 return new OperationResult { IsSuccess = false };

            _repositoryAccessor.MS_Package.Remove(originalItem);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult { IsSuccess = true };
            }
            catch
            {
                return new OperationResult { IsSuccess = false };
            }
        }
        #endregion

        #region Get Data
        public async Task<PaginationUtility<MsPackage>> GetData(PaginationParam pagination, StandardPackingQuantityParam param)
        {
            var pred_MS_Package = PredicateBuilder.New<MsPackage>(true);
            if (!string.IsNullOrEmpty(param.PackageNo))
                pred_MS_Package.And(x => x.PackageNo.Trim().ToLower().Contains(param.PackageNo.Trim().ToLower()));
            if (param.PackageQty != 0)
                pred_MS_Package.And(x => x.PackageQty == param.PackageQty);

            var data = await _repositoryAccessor.MS_Package.FindAll(pred_MS_Package).ToListAsync();

            return PaginationUtility<MsPackage>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsPackage> GetDataOnly(string manuf, string packageNo)
        {
            return await _repositoryAccessor.MS_Package.FirstOrDefaultAsync(item => item.Manuf == manuf.Trim()
            && item.PackageNo == packageNo.Trim());
        }
        #endregion
    }
}