using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_PickingMain_Repository : Repository<MS_QR_PickingMain>, IMS_QR_PickingMain_Repository
    {
        private MyDBContext _context;
        public MS_QR_PickingMain_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}