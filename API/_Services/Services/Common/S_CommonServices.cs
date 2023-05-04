using API._Repositories;
using API._Services.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Common
{
    public class S_CommonServices : I_CommonServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_CommonServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<List<KeyValuePair<string, string>>> GetListBrandName()
        {
            return await _repositoryAccessor.MS_QR_Order.FindAll()
                    .Select(x => new KeyValuePair<string, string>(x.Brandname, x.Brandname)).Distinct().ToListAsync();
        }

        public async Task<List<KeyValuePair<decimal, decimal>>> GetListPackage()
        {
            var data = await _repositoryAccessor.MS_Package.FindAll()
                    .Select(x => new KeyValuePair<decimal, decimal>(x.PackageQty, x.PackageQty)).Distinct().ToListAsync();
            return data.OrderBy(x => x.Value).ToList();
        }

        public async Task<List<KeyValuePair<string, string>>> GetListStatus()
        {
            var data = await _repositoryAccessor.MS_QR_Order.FindAll()
                    .Select(x => new KeyValuePair<string, string>(x.Endcod, x.Endcod)).Distinct().ToListAsync();
            return data.OrderByDescending(x => x.Value).ToList();
        }
    }
}