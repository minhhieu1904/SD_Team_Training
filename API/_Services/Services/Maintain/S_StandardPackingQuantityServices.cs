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

        public async Task<OperationResult> Add(StandardPackingQuantityParam model)
        {
            var originalItem = this._repositoryAccessor.MS_Package.FindAll(x => x.Manuf == "N"
            && x.PackageNo == model.PackageNo.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            model.Manuf = "N";
            var item = Mapper.Map(model).ToANew<MsPackage>(x => x.MapEntityKeys());
            _repositoryAccessor.MS_Package.Add(item);

            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<OperationResult> Update(StandardPackingQuantityParam model)
        {
            var originalItem = this._repositoryAccessor.MS_Package.FirstOrDefault(x => x.Manuf == "N"
            && x.PackageNo == model.PackageNo.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            originalItem.PackageQty = model.PackageQty;
            _repositoryAccessor.MS_Package.Update(originalItem);

            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<OperationResult> Delete(string packageNo)
        {
            var originalItem = await _repositoryAccessor.MS_Package.FirstOrDefaultAsync(x => x.Manuf == "N" && x.PackageNo == packageNo.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            _repositoryAccessor.MS_Package.Remove(originalItem);

            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<PaginationUtility<MsPackage>> GetData(PaginationParam pagination, StandardPackingQuantityParam param)
        {
            var pred_MS_Package = PredicateBuilder.New<MsPackage>(true);
            if (!string.IsNullOrEmpty(param.PackageNo))
                pred_MS_Package.And(x => x.PackageNo == param.PackageNo);
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

    }
}