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
        #region Add
        public async Task<OperationResult> Add(MsDepartment model)
        {
            var originalItem = await _repositoryAccessor.MS_Department.FindAll(x => x.Manuf == model.Manuf.Trim()
            && x.ParNo == model.ParNo.Trim()).FirstOrDefaultAsync();

            if (originalItem != null)
                return new OperationResult { IsSuccess = false, Error = "Data is exists" };

            _repositoryAccessor.MS_Department.Add(model);

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
        public async Task<OperationResult> Update(MsDepartment model)
        {
            var originalItem = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf.Trim() == model.Manuf.Trim()
            && x.ParNo.Trim() == model.ParNo.Trim());

            if (originalItem == null)
                return new OperationResult { IsSuccess = false, Error = "Data Not Found" };

            originalItem.ParName = model.ParName.Trim();
            _repositoryAccessor.MS_Department.Update(originalItem);

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
        public async Task<OperationResult> Delete(string parNo)
        {
            if (string.IsNullOrEmpty(parNo.Trim()))
                return new OperationResult { IsSuccess = false };

            var originalItem = await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(x => x.Manuf == "N"
            && x.ParNo == parNo.Trim());

            if (originalItem == null)
                return new OperationResult { IsSuccess = false };

            _repositoryAccessor.MS_Department.Remove(originalItem);

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
        public async Task<PaginationUtility<MsDepartment>> GetData(PaginationParam pagination, DepartmentDataParam param)
        {
            var pred_MS_Department = PredicateBuilder.New<MsDepartment>(true);
            if (!string.IsNullOrEmpty(param.ParName))
                pred_MS_Department.And(x => x.ParName.Trim().ToLower().Contains(param.ParName.Trim().ToLower()));
            if (!string.IsNullOrEmpty(param.ParNo))
                pred_MS_Department.And(x => x.ParNo.Trim().ToLower().Contains(param.ParNo.Trim().ToLower()));

            var data = await _repositoryAccessor.MS_Department.FindAll(pred_MS_Department).ToListAsync();

            return PaginationUtility<MsDepartment>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MsDepartment> GetDataOnly(string manuf, string parNo)
        {
            return await _repositoryAccessor.MS_Department.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper()
            && item.ParNo.ToUpper() == parNo.ToUpper());
        }
        #endregion
    }
}