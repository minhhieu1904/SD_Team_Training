using API._Repositories;
using API._Services.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Common
{
    public class TransactionCommonService : ITransactionCommonService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public TransactionCommonService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }
       public async Task<List<KeyValuePair<string, string>>> GetListShift()
        {
            return await _repositoryAccessor.MS_Shift.FindAll().Select(x => new KeyValuePair<string, string>(x.Shift, x.ShiftName)).ToListAsync();
        }
    }
}