namespace API.DTOs.Maintain
{
    public class ShiftDataMaintenanceDto
    {
        public string Manuf { get; set; }
        public string Shift { get; set; }
        public string ShiftName { get; set; }
    }

    public class ShiftDataMaintenanceParam
    {
        public string Shift { get; set; }
        public string ShiftName { get; set; }
    }
}