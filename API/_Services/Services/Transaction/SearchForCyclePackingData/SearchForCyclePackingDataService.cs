using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.Transaction.SearchForCyclePackingData;
using API.DTOs.Transaction.SearchForCyclePackingData;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Transaction.SearchForCyclePackingData
{
    public class SearchForCyclePackingDataService : ISearchForCyclePackingDataService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;
        private readonly IFunctionUtility _functionUtility;

        public SearchForCyclePackingDataService(IRepositoryAccessor repository, IConfiguration configuration, IFunctionUtility functionUtility)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value; 
            _functionUtility = functionUtility;
        }

        public Task<OperationResult> CyclePackingPrint(CyclePackingPrint dataPrint)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationUtility<SearchForCyclePackingDataViewModel>> GetDataPagination(PaginationParam pagination, CyclePackingPrintParam param, bool isPaging = true)
        {
            throw new NotImplementedException();
        }

        public async Task<List<KeyValuePair<decimal, decimal>>> GetListPackage()
        {
            var data = await _repository.MS_Package.FindAll(x => x.Manuf == _manuf).Select(x => new KeyValuePair<decimal, decimal>(x.PackageQty, x.PackageQty)).Distinct().ToListAsync();
            return data.OrderBy(x => x.Value).ToList();    
        }
    }
}