
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Department_Repository : Repository<MS_Department> , IMS_Department_Repository
    {
        public MS_Department_Repository(DBContext context) : base(context)
        {
            
        }
    }
}