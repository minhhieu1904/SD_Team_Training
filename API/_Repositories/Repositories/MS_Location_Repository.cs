using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Location_Repository : Repository<MS_Location>, IMS_Location_Repository
    {
        private MyDBContext _context;
        public MS_Location_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}