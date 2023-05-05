using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QrLabel_Repository : Repository<MsQrLabel> , IMS_QrLabel_Repository
    {
        public MS_QrLabel_Repository(DBContext context) : base(context)
        {
        }
    }
}