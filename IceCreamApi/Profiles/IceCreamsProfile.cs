using IceCreamApi.Dtos;
using IceCreamApi.Models;
using AutoMapper;

namespace IceCreamApi.Profiles
{
    public class IceCreamsProfile : Profile
    {
        public IceCreamsProfile()
        {
            CreateMap<IceCream, IceCreamReadDto>();
            CreateMap<IceCreamCreateUpdateDto, IceCream>();
            CreateMap<IceCream, IceCreamCreateUpdateDto>();
        }
    }
}
