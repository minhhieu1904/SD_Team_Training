using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Report_Storage_SumParamRepository :Repository<Report_Storage_SumParam>,IReport_Storage_SumParamRepository
    {
        private DBContext _context;
        public Report_Storage_SumParamRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}