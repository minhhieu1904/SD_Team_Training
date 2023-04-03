using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Location_Repository : Repository<MsLocation>, I_MS_Location_Repository
    {
        public MS_Location_Repository(DBContext context) : base(context)
        {
        }
    }
}