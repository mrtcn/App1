using App1.Models;
using App1.ServiceBase;
using App1.Spot.DbContext;
using App1.Spot.EntityParams;
using App1.Spot.Services.Interfaces;
using App1.Spot.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace App1.Spot.Services
{
    public class SpotService : EntityServiceBase<SpotEntity>, ISpotService
    {
        private readonly IGeoLocationService _geoLocationService;
        private readonly IMapper _mapper;

        private int range { get; } = 20000;

        public SpotService(
            ApplicationDbContext applicationDbContext,
            IGeoLocationService geoLocationService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _geoLocationService = geoLocationService;
            _mapper = mapper;
        }

        public List<SpotEntityParams> GetSpotList(CoordinateModel coordinateModel)
        {
            var spots = Entities.ToList();

            foreach(var spot in spots)
            {
                var distance = _geoLocationService.GetDistance(
                    new GeoCoordinatePortable.GeoCoordinate(spot.Latitude, spot.Longitude),
                    new GeoCoordinatePortable.GeoCoordinate(coordinateModel.Latitude, coordinateModel.Longitude));

                if (distance > range)
                    spots.Remove(spot);
            }
            var spotsEntityParamsList = _mapper.Map<List<SpotEntity>, List<SpotEntityParams>>(spots);

            return spotsEntityParamsList;
        }

        public IEnumerable<PlayerEntityParams> FollowingPlayers(int id)
        {
            var spot = Entities
                .Include(x => x.PlayerSpots)
                .ThenInclude(x => x.Player)
                .FirstOrDefault(x => x.Id == id);
            
            var followingPlayers = spot.PlayerSpots.Select(x => x.Player)?.ToList();

            if (followingPlayers == null || followingPlayers.Count() < 1)
                return null;

            var followingPlayersParamsList = _mapper.Map<List<Player>, List<PlayerEntityParams>>(followingPlayers);
            return followingPlayersParamsList;
        }
    }
}
