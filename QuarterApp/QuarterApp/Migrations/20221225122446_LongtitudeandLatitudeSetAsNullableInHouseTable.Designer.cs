﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuarterApp.DAL;

#nullable disable

namespace QuarterApp.Migrations
{
    [DbContext(typeof(QuarterDbContext))]
    [Migration("20221225122446_LongtitudeandLatitudeSetAsNullableInHouseTable")]
    partial class LongtitudeandLatitudeSetAsNullableInHouseTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuarterApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("QuarterApp.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("QuarterApp.Models.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BathroomCount")
                        .HasColumnType("int");

                    b.Property<int>("BedroomCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BuildYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<decimal>("DiscountPercantage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSpecial")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("StockStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CityId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("QuarterApp.Models.HouseImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("PosterStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("HouseImages");
                });

            modelBuilder.Entity("QuarterApp.Models.SaleManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SaleManagers");
                });

            modelBuilder.Entity("QuarterApp.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("QuarterApp.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstBtnText")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("FirstBtnUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("Order")
                        .HasColumnType("tinyint");

                    b.Property<string>("SecondBtnText")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("SecondBtnUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tittle1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tittle2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("QuarterApp.Models.House", b =>
                {
                    b.HasOne("QuarterApp.Models.Category", "Category")
                        .WithMany("Houses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuarterApp.Models.City", "City")
                        .WithMany("Houses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuarterApp.Models.SaleManager", "Manager")
                        .WithMany("Houses")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("QuarterApp.Models.HouseImage", b =>
                {
                    b.HasOne("QuarterApp.Models.House", "House")
                        .WithMany("HouseImages")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("QuarterApp.Models.Category", b =>
                {
                    b.Navigation("Houses");
                });

            modelBuilder.Entity("QuarterApp.Models.City", b =>
                {
                    b.Navigation("Houses");
                });

            modelBuilder.Entity("QuarterApp.Models.House", b =>
                {
                    b.Navigation("HouseImages");
                });

            modelBuilder.Entity("QuarterApp.Models.SaleManager", b =>
                {
                    b.Navigation("Houses");
                });
#pragma warning restore 612, 618
        }
    }
}
