using App1.Models;
using App1.ServiceBase;
using App1.Spot.EntityParams;

namespace App1.Spot.Services.Interfaces
{
    public interface IPlayerService : IEntityServiceBase<Player>
    {
        PlayerEntityParams GetUserByUserId(string userId);
    }
}
