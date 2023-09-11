namespace API.Helper.Params.ShiftDataMaintain
{
    public class ShiftDataMaintainParam
    {
        public string Shift { get; set; }   
        public string ShiftName { get; set; }
    }
    public class ShiftDataMaintainUploadParam 
    {
        public string Shift { get; set; }
        public IFormFile File { get; set; }
    }
}