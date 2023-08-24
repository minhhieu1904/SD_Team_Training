using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Roles_Repository : Repository<Roles>, IRoles_Repository
    {
        private MyDBContext _context;
        public Roles_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}