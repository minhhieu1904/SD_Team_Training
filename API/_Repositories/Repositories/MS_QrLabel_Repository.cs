
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QrLabel_Repository : Repository<MsQrLabel>, I_MS_QrLabel_Repository
    {
        public MS_QrLabel_Repository(DBContext context) : base(context)
        {
        }
    }
}