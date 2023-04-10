
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QrStorage_Repository : Repository<MsQrStorage>, I_MS_QrStorage_Repository
    {
        public MS_QrStorage_Repository(DBContext context) : base(context)
        {
        }
    }
}