using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class AspNetUser
{
    [Key]
    [StringLength(128)]
    public string Id { get; set; }

    public string Manuf { get; set; }

    public string User_ID { get; set; }

    public string Dept { get; set; }

    public string Use_Stat { get; set; }

    public string Update_By { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Time { get; set; }

    public string EmployeeName { get; set; }

    [StringLength(256)]
    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; }

    public string SecurityStamp { get; set; }

    public string PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LockoutEndDateUtc { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    [Required]
    [StringLength(256)]
    public string UserName { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; } = new List<AspNetUserClaim>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; } = new List<AspNetUserLogin>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();
}
