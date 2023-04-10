

using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QrSort_Repository : Repository<MsQrSort>, I_MS_QrSort_Repository
    {
        public MS_QrSort_Repository(DBContext context) : base(context)
        {
        }
    }
}