using API._Repositories;
using API._Services.Interfaces.common;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.common
{
    public class S_CommonServices : I_CommonServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;

        public S_CommonServices(IRepositoryAccessor repositoryAccessor, IConfiguration configuration)
        {
            _repositoryAccessor = repositoryAccessor;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }

        public async Task<List<KeyValuePair<string, string>>> GetListBrandName()
        {
            return await _repositoryAccessor.MS_QR_Order.FindAll(x => x.manuf == _manuf).Select(x => new KeyValuePair<string, string>(x.brandname, x.brandname)).Distinct().ToListAsync();
        }

        public async Task<List<KeyValuePair<string, string>>> GetListDepartmentName()
        {
            return await _repositoryAccessor.MS_Department.FindAll(x => x.Manuf == _manuf).Select(x => new KeyValuePair<string, string>(x.ParNo, x.ParName)).Distinct().ToListAsync();
        }

        public async Task<List<KeyValuePair<string, string>>> GetListLocationName()
        {
            return await _repositoryAccessor.MS_Location.FindAll(x => x.Manuf == _manuf).Select(x => new KeyValuePair<string, string>(x.StoreH, x.LocationName)).Distinct().ToListAsync();
        }

        public async Task<List<KeyValuePair<string, string>>> GetListShift()
        {
            return await _repositoryAccessor.MS_Shift.FindAll().Select(x => new KeyValuePair<string, string>(x.Shift, x.ShiftName)).ToListAsync();
        }

        public async Task<List<KeyValuePair<string, string>>> GetListTolcls()
        {
            return await _repositoryAccessor.MS_QR_Order.FindAll()
            .Select(x => new KeyValuePair<string, string>(x.tolcls, x.tolcls)).Distinct().ToListAsync();
        }
    }
}