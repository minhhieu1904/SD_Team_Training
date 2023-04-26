using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_CycleRepository : Repository<MS_QR_Cycle>,IMS_QR_CycleRepository
    {
         private DBContext _context;

        public MS_QR_CycleRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}