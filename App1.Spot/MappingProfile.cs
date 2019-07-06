using App1.Models;
using App1.Spot.EntityParams;
using App1.Spot.ViewModels;
using AutoMapper;
using SpotEntity = App1.Spot.Data.Entities.Spot;

namespace Upope.Spot
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerEntityParams>();
            CreateMap<PlayerEntityParams, Player>();

            CreateMap<SpotEntity, SpotEntityParams>();
            CreateMap<SpotEntityParams, SpotEntity>();

            CreateMap<CreateOrUpdatePlayerViewModel, PlayerEntityParams>();
            CreateMap<PlayerEntityParams, CreateOrUpdatePlayerViewModel>();
        }
    }
}
