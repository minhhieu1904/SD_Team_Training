using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.PackingScan;

namespace API._Services.Interfaces.Common
{
    public interface ITransactionCommonService
    {
        Task<List<KeyValuePair<string, string>>> GetListShift();
    }
}