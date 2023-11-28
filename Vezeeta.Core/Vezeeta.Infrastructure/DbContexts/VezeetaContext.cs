using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Infrastructure.DbContexts
{
    public class VezeetaContext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Specialization>  Specializations { get; set; }
        DbSet<Discount> Discounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=VezeetaDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasKey(das => new { das.timeID, das.dayOfWeek });
            base.OnModelCreating(modelBuilder);
        }
    }
}
