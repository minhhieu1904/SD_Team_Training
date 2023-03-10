namespace API._Services.Interfaces
{
    public interface IShiftService
    {
        // vd e muốn lấy danh sách bảng Shift ra
        Task<List<I_Shift_Data_Maintain>> LoadShiftDat ();
    }
}