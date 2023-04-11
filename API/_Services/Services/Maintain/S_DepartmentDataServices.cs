using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.DepartmentDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Maintain
{
    public class S_DepartmentDataServices : I_DepartmentDataServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_DepartmentDataServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Add(MsDepartment model)
        {

            if (!string.IsNullOrEmpty(model.ParName) || !string.IsNullOrEmpty(model.ParNo))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Department.FindAll(x => x.Manuf == model.Manuf.Trim()
            && x.ParNo == model.ParNo.Trim()).FirstOrDefaultAsync();

            if (originalItem != null)
                return new OperationResult(false);

            model.Manuf = "N";
            _repositoryAccessor.MS_Department.Add(model);

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

        public async Task<OperationResult> Update(MsDepartment model)
        {
            if (!string.IsNullOrEmpty(model.ParName) || !string.IsNullOrEmpty(model.ParNo))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf == model.Manuf.Trim()
            && x.ParNo == model.ParNo.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            originalItem.ParName = model.ParName.Trim();
            _repositoryAccessor.MS_Department.Update(originalItem);

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

        public async Task<OperationResult> Delete(string parNo)
        {
            if (!string.IsNullOrEmpty(parNo))
                return new OperationResult(false);

            var originalItem = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf == "N"
            && x.ParNo == parNo.Trim());

            if (originalItem == null)
                return new OperationResult(false);

            _repositoryAccessor.MS_Department.Remove(originalItem);

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

        public async Task<PaginationUtility<MsDepartment>> GetData(PaginationParam pagination, DepartmentDataParam param)
        {
            var pred_MS_Department = PredicateBuilder.New<MsDepartment>(true);
            if (!string.IsNullOrEmpty(param.ParName))
                pred_MS_Department.And(x => x.ParName == param.ParName);
            if (!string.IsNullOrEmpty(param.ParNo))
                pred_MS_Department.And(x => x.ParNo == param.ParNo);

            var data = await _repositoryAccessor.MS_Department.FindAll(pred_MS_Department).ToListAsync();

            return PaginationUtility<MsDepartment>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsDepartment> GetDataOnly(string manuf, string parNo)
        {
            return await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper()
            && item.ParNo.ToUpper() == parNo.ToUpper());
        }

    }
}