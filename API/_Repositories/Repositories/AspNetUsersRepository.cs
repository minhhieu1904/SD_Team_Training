
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class AspNetUsersRepository : Repository<AspNetUsers>, IAspNetUsersRepository
    {
         private DBContext _context;

        public AspNetUsersRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}