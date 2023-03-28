using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserParam
    {
        public string Account {get; set;}
        public string Password {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public bool isActive {get; set;}
        public string updateBy {get; set;}
        public DateTime updateTime {get; set;}
    }
}