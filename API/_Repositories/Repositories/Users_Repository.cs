using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Users_Repository : Repository<Users>, IUsers_Repository
    {
        private MyDBContext _context;
        public Users_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}