using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace App1.Spot.Data.Mappings
{
    public class SpotMapping : IEntityTypeConfiguration<SpotEntity>
    {
        public void Configure(EntityTypeBuilder<SpotEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
