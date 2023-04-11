using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.WarehouseBasicData;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.S_WarehouseBasicDataMaintenance
{
    public class S_WarehouseBasicDataServices : I_WarehouseBasicDataServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_WarehouseBasicDataServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Add(MsLocation model)
        {
            if (!string.IsNullOrEmpty(model.LocationName) || !string.IsNullOrEmpty(model.StoreH))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Location.FindAll(x => x.Manuf == model.Manuf
            && x.StoreH == model.StoreH.Trim()).FirstOrDefaultAsync();

            if (originalItem != null)
                return new OperationResult(false);

            model.Manuf = "N";
            _repositoryAccessor.MS_Location.Add(model);
            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false);
            }

        }

        public async Task<OperationResult> Update(MsLocation model)
        {
            if (!string.IsNullOrEmpty(model.LocationName) || !string.IsNullOrEmpty(model.StoreH))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Location.FirstOrDefaultAsync(x => x.Manuf == model.Manuf.Trim()
             && x.StoreH == model.StoreH.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            originalItem.LocationName = model.LocationName.Trim();

            _repositoryAccessor.MS_Location.Update(originalItem);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false);
            }
        }

        public async Task<OperationResult> Delete(string StoreH)
        {
            if (!string.IsNullOrEmpty(StoreH))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Location.FirstOrDefaultAsync(x => x.Manuf == "N" && x.StoreH == StoreH.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            _repositoryAccessor.MS_Location.Remove(originalItem);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false);
            }
        }

        public async Task<PaginationUtility<MsLocation>> GetData(PaginationParam pagination, WarehouseBasicDataParam param)
        {
            var pred_MS_Location = PredicateBuilder.New<MsLocation>(true);
            if (!string.IsNullOrEmpty(param.StoreH))
                pred_MS_Location.And(x => x.StoreH == param.StoreH);
            if (!string.IsNullOrEmpty(param.LocationName))
                pred_MS_Location.And(x => x.LocationName == param.LocationName);

            var data = await _repositoryAccessor.MS_Location.FindAll(pred_MS_Location).ToListAsync();

            return PaginationUtility<MsLocation>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsLocation> GetDataOnly(string manuf, string StoreH)
        {
            return await _repositoryAccessor.MS_Location.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.StoreH.ToUpper() == StoreH.ToUpper());
        }


    }
}