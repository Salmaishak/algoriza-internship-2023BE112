﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vezeeta.Infrastructure.DbContexts;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    [DbContext(typeof(VezeetaContext))]
    [Migration("20231128225144_updatevalidation")]
    partial class updatevalidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.Property<TimeSpan>("timeID")
                        .HasColumnType("time");

                    b.Property<int>("dayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("discountID")
                        .HasColumnType("int");

                    b.Property<int>("doctorID")
                        .HasColumnType("int");

                    b.Property<int>("patientID")
                        .HasColumnType("int");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("timeID", "dayOfWeek");

                    b.HasIndex("discountID");

                    b.HasIndex("doctorID");

                    b.HasIndex("patientID");

                    b.ToTable("Appointments");
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

                    b.ToTable("Discounts");
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

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.HasBaseType("Vezeeta.Core.Models.User");

                    b.Property<int>("specializationID")
                        .HasColumnType("int");

                    b.HasIndex("specializationID");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Patient", b =>
                {
                    b.HasBaseType("Vezeeta.Core.Models.User");

                    b.Property<int?>("PatientuserId")
                        .HasColumnType("int");

                    b.HasIndex("PatientuserId");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Discount", "discount")
                        .WithMany()
                        .HasForeignKey("discountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Doctor", "doctor")
                        .WithMany("Doctors_Appointments")
                        .HasForeignKey("doctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("patientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("discount");

                    b.Navigation("doctor");

                    b.Navigation("patient");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("specializationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Patient", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Patient", null)
                        .WithMany("Patients")
                        .HasForeignKey("PatientuserId");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Doctor", b =>
                {
                    b.Navigation("Doctors_Appointments");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Patient", b =>
                {
                    b.Navigation("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}
