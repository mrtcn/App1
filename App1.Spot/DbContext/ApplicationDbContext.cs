using App1.Models;
using App1.Spot.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using DbContextClass = Microsoft.EntityFrameworkCore.DbContext;
using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace App1.Spot.DbContext
{
    public class ApplicationDbContext : DbContextClass
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //                     .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();


            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.ApplyConfiguration(configurationInstance);
            //}

            modelBuilder.ApplyConfiguration<SpotEntity>(new SpotMapping());
            modelBuilder.ApplyConfiguration<Player>(new PlayerMapping());
            modelBuilder.ApplyConfiguration<PlayerSpot>(new PlayerSpotMapping());

            modelBuilder.Entity<SpotEntity>().HasData(
                new SpotEntity
                {
                    Id = 1,
                    Name = "Heybeliada Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Status = ServiceBase.Enums.Status.Active,
                    ImageUrl = "",
                    Latitude = 40.871517,
                    Longitude = 29.089059,
                    CreatedDate = DateTime.Now
                },
                new SpotEntity
                {
                    Id = 2,
                    Name = "Kınalıada Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Status = ServiceBase.Enums.Status.Active,
                    ImageUrl = "",
                    Latitude = 40.906027,
                    Longitude = 29.048176,
                    CreatedDate = DateTime.Now
                },
                new SpotEntity
                {
                    Id = 3,
                    Name = "Burgazada Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Status = ServiceBase.Enums.Status.Active,
                    ImageUrl = "",
                    Latitude = 40.880651,
                    Longitude = 29.061132,
                    CreatedDate = DateTime.Now
                },
                new SpotEntity
                {
                    Id = 4,
                    Name = "Suadiye Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Status = ServiceBase.Enums.Status.Active,
                    ImageUrl = "",
                    Latitude = 40.958429,
                    Longitude = 29.081837,
                    CreatedDate = DateTime.Now
                },
                new SpotEntity
                {
                    Id = 5,
                    Name = "Kadıköy Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Status = ServiceBase.Enums.Status.Active,
                    ImageUrl = "",
                    Latitude = 40.991617,
                    Longitude = 29.025069,
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
