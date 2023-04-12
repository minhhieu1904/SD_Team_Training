using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Maintain.DepartmentDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Maintain
{
    public class S_DepartmentDataMaintainServices : I_DepartmentDataMaintainServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_DepartmentDataMaintainServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }
        public async Task<OperationResult> Add(MsDepartment model)
        {
            var original = await _repositoryAccessor.MS_Department.FindAll(
                x=>x.Manuf == model.Manuf 
                && x.ParNo == model.ParNo.Trim()).FirstOrDefaultAsync();
            if (original != null)
            {
                return new OperationResult(false);
            } else {
                model.Manuf = "N";
                _repositoryAccessor.MS_Department.Add(model);
                if(await _repositoryAccessor.Save())
                    return new OperationResult(true);
                return new OperationResult(false);
            }
        }
        public async Task<OperationResult> Update(MsDepartment model)
        {
            var original = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(
                x=>x.Manuf == model.Manuf && x.ParNo == model.ParNo.Trim());
            if(original == null){
                return new OperationResult(false);
            }
            original.ParName = model.ParName.Trim();

            _repositoryAccessor.MS_Department.Update(original);
            if(await _repositoryAccessor.Save())
                    return new OperationResult(true);
                return new OperationResult(false);
        }

        public async Task<PaginationUtility<MsDepartment>> GetData(PaginationParam pagination, DepartmentDataMaintainParam param)
        {
            var pred = PredicateBuilder.New<MsDepartment>(true);
            if(!string.IsNullOrEmpty(param.ParName)){
                pred.And(x=>x.ParName == param.ParName);
            }
            if(!string.IsNullOrEmpty(param.ParNo)){
                pred.And(x=>x.ParNo == param.ParNo);
            }
            var data = await _repositoryAccessor.MS_Department.FindAll(pred).ToListAsync();
            return PaginationUtility<MsDepartment>
            .Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsDepartment> GetDataOnly(string manuf, string parNo)
        {
            return await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper()
            && item.ParNo.ToUpper() == parNo.ToUpper());
        }
    }
}