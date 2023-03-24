using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        protected DBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().
            HasKey(m => new { m.Account });
            modelBuilder.Entity<RoleUser>()
            .HasKey(m => new { m.UserAccount, m.RoleUnique });
            modelBuilder.Entity<Roles>().
            HasKey(m => new { m.RoleUnique });
        }

        //entities
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

    }
}