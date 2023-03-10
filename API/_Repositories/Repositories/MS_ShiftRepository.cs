
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_ShiftRepository : Repository<MS_Shift>, IMS_ShiftRepository
    {
         private DBContext _context;

        public MS_ShiftRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}