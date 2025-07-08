using AutoMapper;
using MyShop.Application.Feature.User.DTOs;

namespace MyShop.Application.Feature.User.MappingProfiles;

public class UserProfiles:Profile
{
    public UserProfiles()
    {
        CreateMap<CreateUserDto, Domain.Entities.UserEntity.User>()
            .ForMember(set => set.CreateDate, opt
                => opt.MapFrom(src => DateTime.Now))
            .ForMember(set => set.IsDelete, opt => opt
                .MapFrom(src => false));
        
        CreateMap<RegisterUserDto, Domain.Entities.UserEntity.User>()
            .ForMember(set => set.CreateDate, opt
                => opt.MapFrom(src => DateTime.Now))
            .ForMember(set => set.IsDelete, opt => opt
                .MapFrom(src => false));
        
        CreateMap<UpdateUserDto, Domain.Entities.UserEntity.User>();
        CreateMap<Domain.Entities.UserEntity.User, UserDto>();

    }
}
