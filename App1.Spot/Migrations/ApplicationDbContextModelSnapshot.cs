﻿// <auto-generated />
using System;
using App1.Spot.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App1.Spot.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("App1.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FirstName");

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<string>("LastName");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Nickname");

                    b.Property<string>("PictureUrl");

                    b.Property<int>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("App1.Models.PlayerSpot", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("SpotId");

                    b.HasKey("PlayerId", "SpotId");

                    b.HasIndex("SpotId");

                    b.ToTable("PlayerSpot");
                });

            modelBuilder.Entity("App1.Spot.Data.Entities.Spot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Spot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(7937),
                            Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                            ImageUrl = "",
                            Latitude = 40.871516999999997,
                            Longitude = 29.089058999999999,
                            Name = "Heybeliada Tennis Club",
                            ShortDescription = "Greatest Tennis Club of the World",
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8444),
                            Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                            ImageUrl = "",
                            Latitude = 40.906027000000002,
                            Longitude = 29.048176000000002,
                            Name = "Kınalıada Tennis Club",
                            ShortDescription = "Greatest Tennis Club of the World",
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8450),
                            Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                            ImageUrl = "",
                            Latitude = 40.880651,
                            Longitude = 29.061132000000001,
                            Name = "Burgazada Tennis Club",
                            ShortDescription = "Greatest Tennis Club of the World",
                            Status = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8453),
                            Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                            ImageUrl = "",
                            Latitude = 40.958429000000002,
                            Longitude = 29.081837,
                            Name = "Suadiye Tennis Club",
                            ShortDescription = "Greatest Tennis Club of the World",
                            Status = 1
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2019, 7, 13, 11, 19, 10, 228, DateTimeKind.Local).AddTicks(8456),
                            Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                            ImageUrl = "",
                            Latitude = 40.991616999999998,
                            Longitude = 29.025068999999998,
                            Name = "Kadıköy Tennis Club",
                            ShortDescription = "Greatest Tennis Club of the World",
                            Status = 1
                        });
                });

            modelBuilder.Entity("App1.Models.PlayerSpot", b =>
                {
                    b.HasOne("App1.Models.Player", "Player")
                        .WithMany("PlayerSpots")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App1.Spot.Data.Entities.Spot", "Spot")
                        .WithMany("PlayerSpots")
                        .HasForeignKey("SpotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
