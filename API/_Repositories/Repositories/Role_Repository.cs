using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Role_Repository : Repository<Role>, I_Role_Repository
    {
        public Role_Repository(DBContext context) : base(context)
        {
        }
    }
}