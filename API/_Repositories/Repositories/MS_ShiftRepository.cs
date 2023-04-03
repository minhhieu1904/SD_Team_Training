using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_ShiftRepository : Repository<MS_Shift>, IMS_ShiftRepository
    {
        public MS_ShiftRepository(DBContext context) : base(context)
        {
        }
    }
}