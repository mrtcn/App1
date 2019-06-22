using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace App1.Models
{
    public class PlayerSpot
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int SpotId { get; set; }
        public SpotEntity Spot { get; set; }
    }
}
