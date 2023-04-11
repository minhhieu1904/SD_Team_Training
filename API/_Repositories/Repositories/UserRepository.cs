
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private MyDBContext _context;

        public UserRepository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}