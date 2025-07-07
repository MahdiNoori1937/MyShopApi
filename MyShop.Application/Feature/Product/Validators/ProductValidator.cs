using MyShop.Application.Feature.User.DTOs;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Application.Feature.User.Validators;

public class CreateProductValidator
{
    private readonly IUserRepository _userRepository;

    public CreateProductValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CreateUserStatusDto> Validate(CreateUserDto createUserDto)
    {
        if (await _userRepository.IsUserExistAsyncEmailOrMobile(createUserDto.Email,createUserDto.Phone))
        {
            return CreateUserStatusDto.DuplicateInformation;
        }

        return CreateUserStatusDto.Success;
    }
}
public class UpdateProductValidator
{
    private readonly IUserRepository _userRepository;

    public UpdateProductValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserStatusDto> Validate(UpdateUserDto UpdateUserDto,Domain.Entities.UserEntity.User? user )
    {
        if (user==null)
        {
            return UpdateUserStatusDto.NotFound;
        }
        bool isEmailChanged = UpdateUserDto.Email != user.Email;
        bool isMobileChanged = UpdateUserDto.Phone != user.Phone;
        string? emailToCheck = isEmailChanged ? UpdateUserDto.Email : null;
        string? mobileToCheck = isMobileChanged ? UpdateUserDto.Phone : null;
        
        if ((isEmailChanged || isMobileChanged) &&
            await _userRepository.IsUserExistAsyncEmailOrMobile(emailToCheck, mobileToCheck))
        {
            return UpdateUserStatusDto.DuplicateInformation;
        }

        return UpdateUserStatusDto.Success;
    }
}