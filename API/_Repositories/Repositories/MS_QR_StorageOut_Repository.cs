using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_StorageOut_Repository : Repository<MS_QR_StorageOut>, IMS_QR_StorageOut_Repository
    {
        private MyDBContext _context;
        public MS_QR_StorageOut_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}