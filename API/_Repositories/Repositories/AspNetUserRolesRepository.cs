using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class AspNetUserRolesRepository : Repository<AspNetUserRoles> , IAspNetUserRolesRepository
    {
         private DBContext _context;

        public AspNetUserRolesRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}