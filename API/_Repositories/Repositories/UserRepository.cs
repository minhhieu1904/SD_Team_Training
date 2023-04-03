
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private DBContext _context;

        public UserRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}