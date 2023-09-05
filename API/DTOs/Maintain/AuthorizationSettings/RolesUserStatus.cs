namespace API.Dtos.Maintain.AuthorizationSetting
{
    public class RolesUserStatus
    {
        public string user_account {get;set;}
        public string role_unique {get;set;}
        public string role_name {get;set;}
        public double role_sequence {get;set;}
        public bool status {get;set;}
    }
}