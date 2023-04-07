using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_DepartmentRepository : Repository<MS_Department>, IMS_DepartmentRepository
    {
        public MS_DepartmentRepository(DBContext context) : base(context)
        {
        }
    }
}