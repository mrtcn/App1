using GeoCoordinatePortable;

namespace App1.Spot.Services.Interfaces
{
    public interface IGeoLocationService
    {
        double GetDistance(GeoCoordinate actualCoordinates, GeoCoordinate destinationCoordinates);
    }
}
