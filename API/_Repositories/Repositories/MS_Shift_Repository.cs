using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Shift_Repository : Repository<MS_Shift>, IMS_Shift_Repository
    {
        public MS_Shift_Repository(DBContext context) : base(context)
        {
        }
    }
}