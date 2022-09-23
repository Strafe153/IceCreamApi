using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace WebApi.AutoMapperProfiles;

public class IceCreamProfile : Profile
{
    public IceCreamProfile()
    {
        CreateMap<IceCream, IceCreamReadDto>();
        CreateMap<IceCream, IceCreamCreateUpdateDto>().ReverseMap();
    }
}
