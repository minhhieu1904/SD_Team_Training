﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class AspNetUserClaim
    {
        [Key]
        public int Id { get; set; }
        [StringLength(128)]
        public string UserId { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("AspNetUserClaims")]
        public virtual AspNetUser User { get; set; } = null!;
    }
}
