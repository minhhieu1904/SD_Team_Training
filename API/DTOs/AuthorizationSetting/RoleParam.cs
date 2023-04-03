namespace API.DTOs.AuthorizationSetting
{
    public class RoleParam
    {
        public string UserAccount { get; set; }
        public string RoleUnique { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string RoleNote { get; set; }
        public double RoleSequence { get; set; }
        public bool IsCheck { get; set; }
    }
}