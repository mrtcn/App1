using System.Threading.Tasks;

namespace App1.Spot.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
    }
}
