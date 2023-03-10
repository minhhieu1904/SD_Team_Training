
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Location_Respository : Repository<MS_Location> , IMS_Location_Repository
    {
        public MS_Location_Respository(DBContext context) : base(context)
        {
            
        }
    }
}