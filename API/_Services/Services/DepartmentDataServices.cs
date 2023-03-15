
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using LinqKit;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class DepartmentDataServices : IDepartmentDataServices
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public DepartmentDataServices(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }

        public async Task<OperationResult> Add(MS_Department parNo)
        {
            var data = await _repoAccessor.MS_Department.FindSingle(x=> x.ParNo == parNo.ParNo);
            if (data != null)
            {
                return new OperationResult(false, "đã tồn tại" );      
            }
            else{
                var item = Mapper.Map(parNo).ToANew<MS_Department>(x=> x.MapEntityKeys());
                _repoAccessor.MS_Department.Add(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true,"Add thành công");
        }
        public async Task<PaginationUtility<MS_Department>> LoadData(PaginationParam paginationParams, string parNo, string parName)
        {
            var pred = PredicateBuilder.New<MS_Department>(true);
            if(!string.IsNullOrEmpty(parNo))
            {
                pred.And(x => x.ParNo == parNo.Trim() );
            }
            if(!string.IsNullOrEmpty(parName))
            {
                pred.And(x => x.ParName == parName.Trim() );
            }
            var data = _repoAccessor.MS_Department.FindAll(pred);
            return await PaginationUtility<MS_Department>.CreateAsync(data, paginationParams.PageNumber, paginationParams.PageSize, true);

        }

        public async Task<OperationResult> Update(MS_Department parNo)
        {
            var item = await _repoAccessor.MS_Department.FirstOrDefaultAsync(x=> x.Manuf.Trim() == "N" && x.ParNo== parNo.ParNo);
            if(item == null)
            {
                return new OperationResult(false);
            }
            else{
                item.ParName=parNo.ParName.Trim();
                _repoAccessor.MS_Department.Update(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true, "Update thành công");
        }
    }
}