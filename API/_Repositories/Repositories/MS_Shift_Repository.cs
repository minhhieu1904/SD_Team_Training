using API.Data;
using API._Repositories.Interfaces;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_Shift_Repository : Repository<MsShift>, I_MS_Shift_Repository
    {
        public MS_Shift_Repository(DBContext context) : base(context)
        {
        }
    }
}