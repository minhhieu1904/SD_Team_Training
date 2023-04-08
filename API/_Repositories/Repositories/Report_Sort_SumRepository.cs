
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Report_Sort_SumRepository : Repository<Report_Sort_SumParam>,IReport_Sort_SumRepository
    {
         private DBContext _context;

        public Report_Sort_SumRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}