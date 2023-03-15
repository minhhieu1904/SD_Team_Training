using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MS_Location
    {
        [Key]
        public string Manuf { get; set; }
        [Key]
        public string StoreH { get; set; }
        public string LocationName { get; set; }
    }
}