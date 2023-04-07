using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_Storage_Repository : Repository<MS_QR_Storage>, IMS_QR_Storage_Repository
    {
        DBContext _context;
        public MS_QR_Storage_Repository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}