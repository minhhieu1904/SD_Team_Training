using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class RoleUser_Repository : Repository<RoleUser>, I_RoleUser_Repository
    {
        public RoleUser_Repository(DBContext context) : base(context)
        {
        }
    }
}