
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("__MigrationHistory")]
    public partial class MigrationHistory
    {
        [Key]
        [StringLength(150)]
        public string MigrationId { get; set; } = null!;
        [Key]
        [StringLength(300)]
        public string ContextKey { get; set; } = null!;
        public byte[] Model { get; set; } = null!;
        [StringLength(32)]
        public string ProductVersion { get; set; } = null!;
    }
}
