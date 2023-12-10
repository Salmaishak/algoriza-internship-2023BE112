using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Infrastructure.DbContexts 
{
    public class VezeetaContext : IdentityDbContext<IdentityUser>
    {
        public VezeetaContext(DbContextOptions<VezeetaContext> options ) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<User> AllUsers
        {
            get { return Users; }
             set { Users = value; }
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Appointment> AllAppointments
        {
            get { return Appointments; }
             set { Appointments = value; }
        }
        public DbSet<Specialization>  Specializations { get; set; }
       public DbSet<Discount> Discounts { get; set; }
        public DbSet<Specialization> AllSpecializations
        {
            get { return Specializations; }
             set { Specializations = value; }
        }

        public DbSet<Discount> AllDiscounts
        {
            get { return Discounts; }
             set { Discounts = value; }
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Doctor> AllDoctors
        {
            get { return Doctors; }
             set { Doctors = value; }
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Booking> AllBookings
        {
            get { return Bookings; }
             set { Bookings = value; }
        }

        public DbSet<TimeSlot> AllTimeSlots
        {
            get { return TimeSlots; }
             set { TimeSlots = value; }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=VezeetaDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("Users"); // Mapping Users to the Users table
            modelBuilder.Entity<Doctor>().ToTable("Doctors"); // Mapping Doctors to a separate Doctors table
            base.OnModelCreating(modelBuilder);

        }
    }
}
