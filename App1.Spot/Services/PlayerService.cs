using App1.Models;
using App1.ServiceBase;
using App1.Spot.DbContext;
using App1.Spot.EntityParams;
using App1.Spot.Services.Interfaces;
using AutoMapper;
using System.Linq;

namespace App1.Spot.Services
{
    public class PlayerService : EntityServiceBase<Player>, IPlayerService
    {
        private readonly IMapper _mapper;
        public PlayerService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
        }

        public PlayerEntityParams GetUserByUserId(string userId)
        {
            var user = Entities.FirstOrDefault(x => x.UserId == userId);
            var userParams = _mapper.Map<Player, PlayerEntityParams>(user);

            return userParams;
        }
    }
}
