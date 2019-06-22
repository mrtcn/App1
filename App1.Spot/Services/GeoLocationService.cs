using App1.Spot.Services.Interfaces;
using GeoCoordinatePortable;

namespace App1.Spot.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        public double GetDistance(GeoCoordinate actualCoordinates, GeoCoordinate destinationCoordinates)
        {
            return actualCoordinates.GetDistanceTo(destinationCoordinates);
        }
    }
}
