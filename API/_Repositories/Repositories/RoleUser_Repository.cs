using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class RoleUser_Repository : Repository<RoleUser>, IRoleUser_Repository
    {
        private MyDBContext _context;
        public RoleUser_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}