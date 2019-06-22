using App1.Models;
using App1.Spot.Data.Mappings;
using Microsoft.EntityFrameworkCore;
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
        }
    }
}
