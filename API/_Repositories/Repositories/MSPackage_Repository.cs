using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MSPackage_Repository : Repository<MS_Package>, IMSPackage_Repository
    {
        public MSPackage_Repository(DBContext context) : base(context)
        {

        }
    }
}