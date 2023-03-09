using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MSShift_Repository : Repository<MS_Shift>, IMSShift_Repository
    {
        public MSShift_Repository(DBContext context) : base(context)
        {
        }
    }
}