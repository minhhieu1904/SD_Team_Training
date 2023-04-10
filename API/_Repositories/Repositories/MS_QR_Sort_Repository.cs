using API._Repositories.Interfaces;
using API.Data;

namespace API._Repositories.Repositories
{
    public class MS_QR_Sort_Repository : Repository<MS_QR_Sort>, IMS_QR_Sort_Repository
    {
        private DBContext _context;

        public MS_QR_Sort_Repository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}