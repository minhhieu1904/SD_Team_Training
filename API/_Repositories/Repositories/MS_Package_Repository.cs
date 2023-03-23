using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Package_Repository : Repository<MS_Package>, IMS_Package_Repository
    {
        private MyDBContext _context;
        public MS_Package_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}