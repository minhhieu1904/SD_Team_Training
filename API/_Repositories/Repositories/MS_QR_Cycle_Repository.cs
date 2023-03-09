using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_Cycle_Repository : Repository<MS_QR_Cycle>, IMS_QR_Cycle_Repository
    {
        private MyDBContext _context;
        public MS_QR_Cycle_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}