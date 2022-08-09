using AutoMapper;
using Core.Entities;
using Core.ViewModels;

namespace WebApi.AutoMapperProfiles
{
    public class IceCreamProfile : Profile
    {
        public IceCreamProfile()
        {
            CreateMap<IceCream, IceCreamReadViewModel>();
            CreateMap<IceCream, IceCreamCreateUpdateViewModel>().ReverseMap();
        }
    }
}
