using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;

namespace App1.Interfaces
{
    public interface ILocationService
    {
        Task<Position> GetCurrentLocation();
    }
}
