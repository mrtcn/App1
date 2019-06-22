using AutoMapper;
using App1.Identity.Entities;
using App1.Identity.ViewModels;

namespace Upope.Identity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProfileViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, ProfileViewModel>();

            CreateMap<ApplicationUser, CreateOrUpdateSpotPlayerViewModel>();
            CreateMap<CreateOrUpdateSpotPlayerViewModel, ApplicationUser>();
        }
    }
}
