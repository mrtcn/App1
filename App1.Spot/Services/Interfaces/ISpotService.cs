using App1.ServiceBase;
using App1.Spot.EntityParams;
using App1.Spot.ViewModels;
using System.Collections.Generic;
using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace App1.Spot.Services.Interfaces
{
    public interface ISpotService : IEntityServiceBase<SpotEntity>
    {
        List<SpotEntityParams> GetSpotList(string userId, CoordinateModel coordinateModel);
    }
}
