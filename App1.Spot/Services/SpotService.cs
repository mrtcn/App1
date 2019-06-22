using App1.ServiceBase;
using App1.Spot.DbContext;
using App1.Spot.EntityParams;
using App1.Spot.Services.Interfaces;
using App1.Spot.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace App1.Spot.Services
{
    public class SpotService : EntityServiceBase<SpotEntity>, ISpotService
    {
        private readonly IGeoLocationService _geoLocationService;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        private int range { get; } = 20000;

        public SpotService(
            ApplicationDbContext applicationDbContext,
            IGeoLocationService geoLocationService,
            IPlayerService playerService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _geoLocationService = geoLocationService;
            _playerService = playerService;
            _mapper = mapper;
        }

        public List<SpotEntityParams> GetSpotList(string userId, CoordinateModel coordinateModel)
        {
            var spots = Entities.ToList();

            foreach(var spot in spots)
            {
                var destinationUser = _playerService.GetUserByUserId(userId);
                var distance = _geoLocationService.GetDistance(
                    new GeoCoordinatePortable.GeoCoordinate(spot.Latitude, spot.Longitude),
                    new GeoCoordinatePortable.GeoCoordinate(destinationUser.Latitude, destinationUser.Longitude));

                if (distance > range)
                    spots.Remove(spot);
            }
            var spotsEntityParamsList = _mapper.Map<List<SpotEntity>, List<SpotEntityParams>>(spots);

            return spotsEntityParamsList;
        }
    }
}
