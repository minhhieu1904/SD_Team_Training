using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            Users = new HashSet<AspNetUser>();
        }

        [Key]
        [StringLength(128)]
        public string Id { get; set; } = null!;
        [StringLength(256)]
        public string Name { get; set; } = null!;

        [ForeignKey("RoleId")]
        [InverseProperty("Roles")]
        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
