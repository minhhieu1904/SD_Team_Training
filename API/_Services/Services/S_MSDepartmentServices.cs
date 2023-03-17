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
    public class SMSDepartmentServices : IMSDepartmentServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public SMSDepartmentServices(IRepositoryAccessor repositoryAccessor) 
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> AddNew(MS_Department model)
        {
            var item = await _repositoryAccessor.MSDepartmet.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.ParNo.Trim() == model.ParNo.Trim());
            if(item != null)
            return new OperationResult(false);
            model.Manuf = "N";
            _repositoryAccessor.MSDepartmet.Add(model);
            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<MS_Department> GetItem(string manuf, string parNo)
        {
            return await _repositoryAccessor.MSDepartmet.FirstOrDefaultAsync(x => x.Manuf == manuf && x.ParNo == parNo);
        }

        public async Task<PaginationUtility<MS_Department>> GetPagingData(PaginationParam param, string parNo, string parName)
        {
            var data = await _repositoryAccessor.MSDepartmet.FindAll().ToListAsync();
            if(!string.IsNullOrEmpty(parNo))
            data = data.Where(x => x.ParNo == parNo).ToList();
            if(!string.IsNullOrEmpty(parName))
            data = data.Where(x => x.ParName.Contains(parName)).ToList();
            return PaginationUtility<MS_Department>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<OperationResult> Update(MS_Department model)
        {
            var data = await _repositoryAccessor.MSDepartmet.FirstOrDefaultAsync(x => x.Manuf == model.Manuf && x.ParNo == model.ParNo);
            if(data == null)
            return new OperationResult(false);
            data.ParName = model.ParName;
            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }
    }
}