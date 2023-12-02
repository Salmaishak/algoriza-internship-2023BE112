﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vezeeta.Infrastructure.DbContexts;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    [DbContext(typeof(VezeetaContext))]
    partial class VezeetaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<int>("doctorID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("doctorID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingID"), 1L, 1);

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.Property<int>("discountId")
                        .HasColumnType("int");

                    b.Property<float>("finalPrice")
                        .HasColumnType("real");

                    b.Property<int>("patientID")
                        .HasColumnType("int");

                    b.Property<int>("timeSlotID")
                        .HasColumnType("int");

                    b.HasKey("BookingID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("discountId");

                    b.HasIndex("patientID");

                    b.HasIndex("timeSlotID");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Discount", b =>
                {
                    b.Property<int>("discountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("discountID"), 1L, 1);

                    b.Property<int>("discountActivity")
                        .HasColumnType("int");

                    b.Property<string>("discountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("discountType")
                        .HasColumnType("int");

                    b.Property<int>("numOfRequests")
                        .HasColumnType("int");

                    b.Property<int>("valueOfDiscount")
                        .HasColumnType("int");

                    b.HasKey("discountID");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Specialization", b =>
                {
                    b.Property<int>("specializationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("specializationID"), 1L, 1);

                    b.Property<string>("specializationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("specializationID");

                    b.ToTable("Specialization");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.TimeSlot", b =>
                {
                    b.Property<int>("SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotId"), 1L, 1);

                    b.Property<int>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("SlotId");

                    b.HasIndex("AppointmentID");

                    b.ToTable("TimeSlot");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"), 1L, 1);

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("userId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.HasBaseType("Vezeeta.Core.Models.User");

                    b.Property<int>("doctorid")
                        .HasColumnType("int");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("specializationID")
                        .HasColumnType("int");

                    b.HasIndex("specializationID");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Doctor", "doctor")
                        .WithMany("Doctors_Appointments")
                        .HasForeignKey("doctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Booking", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Discount", "discount")
                        .WithMany()
                        .HasForeignKey("discountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.User", "Patient")
                        .WithMany()
                        .HasForeignKey("patientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.TimeSlot", "timeslot")
                        .WithMany()
                        .HasForeignKey("timeSlotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("discount");

                    b.Navigation("timeslot");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.TimeSlot", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("specializationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Vezeeta.Core.Models.Doctor", "userId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.Navigation("Doctors_Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
