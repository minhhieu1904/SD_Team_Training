
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Report_wksh_SumParamRepository : Repository<Report_wksh_SumParam>, IReport_wksh_SumParamRepository
    {
        private DBContext _context;

        public Report_wksh_SumParamRepository(DBContext context) : base(context)
        {
            _context = context;
        }

    
    }
}