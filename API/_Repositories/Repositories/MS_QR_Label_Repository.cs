using API._Repositories.Interfaces;
using API.Data;

namespace API._Repositories.Repositories
{
    public class MS_QR_Label_Repository : Repository<MS_QR_Label>, IMS_QR_Label_Repository
    {
        private MyDBContext _context;
        public MS_QR_Label_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}