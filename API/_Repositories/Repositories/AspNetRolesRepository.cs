
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class AspNetRolesRepository : Repository<AspNetRoles>, IAspNetRolesRepository
    {
        private DBContext _context;

        public AspNetRolesRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}