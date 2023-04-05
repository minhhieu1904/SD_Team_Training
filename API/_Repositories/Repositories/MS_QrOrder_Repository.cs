using API._Repositories.Interfaces;
using API.Data;
using API.Models;
namespace API._Repositories.Repositories
{
    public class MS_QrOrder_Repository : Repository<MsQrOrder>, I_MS_QrOrder_Repository
    {
        public MS_QrOrder_Repository(DBContext context) : base(context)
        {
        }
    }
}