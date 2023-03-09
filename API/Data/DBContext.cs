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
        public DBContext(DbContextOptions<DBContext> options): base(options)
        { 

        }

        public DBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public virtual DbSet<MS_Shift> MSShift { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MS_Shift>()
            .HasKey(m => new { m.Manuf, m.Shift });
        }

        //entities
        public DbSet<MS_Shift> MSShifts { get; set; }
    }
}