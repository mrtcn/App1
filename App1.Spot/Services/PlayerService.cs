using App1.Models;
using App1.ServiceBase;
using App1.Spot.DbContext;
using App1.Spot.EntityParams;
using App1.Spot.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Spot.Services
{
    public class PlayerService : EntityServiceBase<Player>, IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public PlayerService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public PlayerEntityParams GetUserByUserId(string userId)
        {
            var user = Entities.FirstOrDefault(x => x.UserId == userId);
            var userParams = _mapper.Map<Player, PlayerEntityParams>(user);
            
            return userParams;
        }

        public async Task<bool> AddPlayerSpot(string userId, int spotId)
        {
            var user = Entities
                .Include(x => x.PlayerSpots)
                .FirstOrDefault(x => x.UserId == userId);

            if (user == null)
                return false;

            if (user.PlayerSpots != null && user.PlayerSpots.Any(x => x.PlayerId == user.Id && x.SpotId == spotId))
                return false;

            var playerSpot = new PlayerSpot()
            {
                PlayerId = user.Id,
                SpotId = spotId
            };

            user.PlayerSpots.Add(playerSpot);
            var result = await _applicationDbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
