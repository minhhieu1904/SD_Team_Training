
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class AspNetUserLoginsRepository : Repository<AspNetUserLogins>, IAspNetUserLoginsrepository
    {
        private DBContext _context;

        public AspNetUserLoginsRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}