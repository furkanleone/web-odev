﻿// <auto-generated />
using System;
using KuaforYonetim.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KuaforYonetim.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241224174100_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KuaforYonetim.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SelectedTimeSlot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerName = "Mehmet",
                            Date = new DateTime(2024, 12, 25, 20, 40, 59, 727, DateTimeKind.Local).AddTicks(5389),
                            EmployeeId = 1,
                            Phone = "5551234567",
                            SelectedTimeSlot = "09:00 - 10:00",
                            Service = "Saç Kesimi",
                            Status = "Confirmed"
                        },
                        new
                        {
                            Id = 2,
                            CustomerName = "Zeynep",
                            Date = new DateTime(2024, 12, 26, 20, 40, 59, 729, DateTimeKind.Local).AddTicks(305),
                            EmployeeId = 2,
                            Phone = "5559876543",
                            SelectedTimeSlot = "10:00 - 11:00",
                            Service = "Makyaj",
                            Status = "Pending"
                        });
                });

            modelBuilder.Entity("KuaforYonetim.Models.AvailableSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AvailableSlots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Day = 1,
                            EmployeeId = 1,
                            EndTime = new TimeSpan(0, 10, 0, 0, 0),
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            Day = 1,
                            EmployeeId = 1,
                            EndTime = new TimeSpan(0, 11, 0, 0, 0),
                            StartTime = new TimeSpan(0, 10, 0, 0, 0)
                        },
                        new
                        {
                            Id = 6,
                            Day = 1,
                            EmployeeId = 2,
                            EndTime = new TimeSpan(0, 10, 0, 0, 0),
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 9,
                            Day = 1,
                            EmployeeId = 3,
                            EndTime = new TimeSpan(0, 10, 0, 0, 0),
                            StartTime = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            Id = 12,
                            Day = 1,
                            EmployeeId = 4,
                            EndTime = new TimeSpan(0, 11, 0, 0, 0),
                            StartTime = new TimeSpan(0, 10, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("KuaforYonetim.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "mehmet@example.com",
                            Name = "Mehmet Topal(Serdivan)",
                            Phone = "5369874521",
                            Position = "Saç Kesimi"
                        },
                        new
                        {
                            Id = 2,
                            Email = "mustafa@example.com",
                            Name = "Mustafa Aslan(Serdivan)",
                            Phone = "5427895686",
                            Position = "Saç Kesimi"
                        },
                        new
                        {
                            Id = 3,
                            Email = "mustafa@example.com",
                            Name = "Cüneyt Akif(Arifiye)",
                            Phone = "5239687542",
                            Position = "Saç Kesimi"
                        },
                        new
                        {
                            Id = 4,
                            Email = "mustafa@example.com",
                            Name = "Samet Akpınar(Arifiye)",
                            Phone = "5648569575",
                            Position = "Saç Kesimi"
                        });
                });

            modelBuilder.Entity("KuaforYonetim.Models.Salon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Salons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Serdivan, Sincan Mahallesi",
                            Image = "serdivan4.jpg",
                            Name = "Serdivan Salon",
                            Phone = "0312-123-4567",
                            WorkingHours = "09:00 - 21:00"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Arifiye, Arifbey Mahallesi",
                            Image = "arifiye.jpg",
                            Name = "Arifiye Salon",
                            Phone = "0216-987-6543",
                            WorkingHours = "10:00 - 20:00"
                        });
                });

            modelBuilder.Entity("KuaforYonetim.Models.Appointment", b =>
                {
                    b.HasOne("KuaforYonetim.Models.Employee", "Employee")
                        .WithMany("Appointments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("KuaforYonetim.Models.AvailableSlot", b =>
                {
                    b.HasOne("KuaforYonetim.Models.Employee", "Employee")
                        .WithMany("Availability")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("KuaforYonetim.Models.Employee", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Availability");
                });
#pragma warning restore 612, 618
        }
    }
}