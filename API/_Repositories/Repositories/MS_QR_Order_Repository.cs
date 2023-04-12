using API._Repositories.Interfaces;
using API.Data;

namespace API._Repositories.Repositories
{
    public class MS_QR_Order_Repository : Repository<MS_QR_Order>, IMS_QR_Order_Repository
    {
        private MyDBContext _context;
        public MS_QR_Order_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}