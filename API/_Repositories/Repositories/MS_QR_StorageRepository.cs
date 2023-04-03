
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_StorageRepository : Repository<MS_QR_Storage>, IMS_QR_StorageRepository
    {

        private DBContext _context;

        public MS_QR_StorageRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}