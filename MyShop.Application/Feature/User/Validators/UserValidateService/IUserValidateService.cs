using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.User.DTOs;

namespace MyShop.Application.Feature.User.Validators.UserValidateService;

public interface IUserValidateService
{
    Task<DeleteUserStatusDto> DeleteValidate(Domain.Entities.UserEntity.User? user);

    Task<UpdateUserStatusDto> UpdateValidate(UpdateUserDto UpdateUserDto, Domain.Entities.UserEntity.User? user);

    Task<CreateUserStatusDto> CreateValidate(CreateUserDto createUserDto);

    Task<LoginUserStatusDto> LoginValidate(LoginUserDto Model, Domain.Entities.UserEntity.User? user);

    Task<RegisterUserStatusDto> RegisterValidate(RegisterUserDto registerUserDto);
}