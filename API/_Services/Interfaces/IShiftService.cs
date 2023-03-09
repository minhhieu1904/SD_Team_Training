using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API._Services.Interfaces
{
    public interface IShiftService
    {
        // vd e muốn lấy danh sách bảng Shift ra
        Task<List<I_Shift_Data_Maintain>> LoadShiftDat ();
    }
}