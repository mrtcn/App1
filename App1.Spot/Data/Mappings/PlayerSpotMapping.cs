using App1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App1.Spot.Data.Mappings
{
    public class PlayerSpotMapping : IEntityTypeConfiguration<PlayerSpot>
    {
        public void Configure(EntityTypeBuilder<PlayerSpot> builder)
        {
            builder.HasKey(x => new { x.PlayerId, x.SpotId});
            builder.HasOne(x => x.Spot).WithMany(x => x.PlayerSpots).HasForeignKey(x => x.SpotId);
            builder.HasOne(x => x.Player).WithMany(x => x.PlayerSpots).HasForeignKey(x => x.PlayerId);
        }
    }
}
