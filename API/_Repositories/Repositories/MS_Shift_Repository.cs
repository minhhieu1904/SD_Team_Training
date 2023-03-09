using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Shift_Repository : Repository<MS_Shift>, IMS_Shift_Repository
    {
        private MyDBContext _context;
        public MS_Shift_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}