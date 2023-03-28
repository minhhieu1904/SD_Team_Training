using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            Roles = new HashSet<AspNetRole>();
        }

        [Key]
        [StringLength(128)]
        public string Id { get; set; } = null!;
        public string? Manuf { get; set; }
        [Column("User_ID")]
        public string? UserId { get; set; }
        public string? Dept { get; set; }
        [Column("Use_Stat")]
        public string? UseStat { get; set; }
        [Column("Update_By")]
        public string? UpdateBy { get; set; }
        [Column("Update_Time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
        public string? EmployeeName { get; set; }
        [StringLength(256)]
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [StringLength(256)]
        public string UserName { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Users")]
        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
