using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_Order_Log_OnlyForST_Repository : Repository<MS_QR_Order_Log_OnlyForST>, IMS_QR_Order_Log_OnlyForST_Repository
    {
        private MyDBContext _context;
        public MS_QR_Order_Log_OnlyForST_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}