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
        public SMSLocationServices(IRepositoryAccessor repositoryAccessor) {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> AddNew(MS_Location model)
        {
            var originalItem = await _repositoryAccessor.MSLocation.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.StoreH.Trim() == model.StoreH.Trim());
            if(originalItem != null)
            return new OperationResult(false);
            model.Manuf = "N";
            _repositoryAccessor.MSLocation.Add(model);
            if(await _repositoryAccessor.Save())
            return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<PaginationUtility<MS_Location>> GetDataPaging(PaginationParam param, string manuf, string locationName)
        {
            var data = await _repositoryAccessor.MSLocation.FindAll().ToListAsync();
            if(!string.IsNullOrEmpty(manuf))
             data = data.Where(x => x.Manuf == manuf).ToList();
             if(!string.IsNullOrEmpty(locationName))
             data = data.Where(x => x.LocationName.Contains(locationName)).ToList();
             return PaginationUtility<MS_Location>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<MS_Location> GetItem(string manuf, string storeH)
        {
           return await _repositoryAccessor.MSLocation.FirstOrDefaultAsync(x => x.Manuf == manuf && x.StoreH == storeH);
        }

        public async Task<OperationResult> UpdateWarehouse(MS_Location model)
        {
            var msLocation = await _repositoryAccessor.MSLocation.FirstOrDefaultAsync( x => x.Manuf == model.Manuf && x.StoreH == model.StoreH);
            if(msLocation == null)
            return new OperationResult(false);
            msLocation.LocationName = model.LocationName;
            if(await _repositoryAccessor.Save())
            return new OperationResult(true);
            return new OperationResult(false);
        }
    }
}