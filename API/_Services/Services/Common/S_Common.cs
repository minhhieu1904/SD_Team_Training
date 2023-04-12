using API._Repositories;
using API._Services.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Common
{
    public class S_Common : I_CommonService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IConfiguration _configuration;
        protected readonly string _manuf;

        public S_Common(
            IRepositoryAccessor respositoryAccessor,
            IConfiguration configuration
        )
        {
            _repositoryAccessor = respositoryAccessor;
            _configuration = configuration;
        }

        #region GetListBrandName
        public async Task<List<KeyValuePair<string, string>>> GetListBrandName()
        {
            return await _repositoryAccessor.MS_QR_Order.FindAll().Select(x => new KeyValuePair<string, string>(x.brandname, x.brandname)).Distinct().ToListAsync();
        }
        #endregion
    }
}