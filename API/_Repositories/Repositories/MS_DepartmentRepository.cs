
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_DepartmentRepository : Repository<MS_Department>, IMS_DepartmentRepository
    {
        private DBContext _context;

        public MS_DepartmentRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}