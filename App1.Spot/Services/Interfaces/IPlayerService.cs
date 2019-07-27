using App1.Models;
using App1.ServiceBase;
using App1.Spot.EntityParams;
using System.Threading.Tasks;

namespace App1.Spot.Services.Interfaces
{
    public interface IPlayerService : IEntityServiceBase<Player>
    {
        PlayerEntityParams GetUserByUserId(string userId);
        Task<bool> AddPlayerSpot(string userId, int spotId);
    }
}
