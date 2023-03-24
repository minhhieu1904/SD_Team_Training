using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Package_Repository : Repository<MsPackage>, I_MS_Package_Repository
    {
        public MS_Package_Repository(DBContext context) : base(context)
        {
        }
    }
}