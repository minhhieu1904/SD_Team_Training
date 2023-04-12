using API._Repositories.Interfaces;
using API.Data;

namespace API._Repositories.Repositories
{
    public class MS_QR_Sort_Repository : Repository<MS_QR_Sort>, IMS_QR_Sort_Repository
    {
        private MyDBContext _context;
        public MS_QR_Sort_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}