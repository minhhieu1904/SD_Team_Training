using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_Label_Repository : Repository<MS_QR_Label>, IMS_QR_Label_Repository
    {
        public MS_QR_Label_Repository(DBContext context) : base(context)
        {
            
        }
    }
}