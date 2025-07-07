using MyShop.Application.Commonn.Security;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Application.Feature.User.Validators;

public class CreateUserValidator
{
    private readonly IUserRepository _userRepository;

    public CreateUserValidator(IUserRepository userRepository)
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
public class UpdateUserValidator
{
    private readonly IUserRepository _userRepository;

    public UpdateUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserStatusDto> Validate(UpdateUserDto UpdateUserDto,Domain.Entities.UserEntity.User? user )
    {
        if (user==null)
        {
            return UpdateUserStatusDto.NotFound;
        }

        if (!SecretHasher.Verify(UpdateUserDto.OldPassword, user.Password))
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

public class DeleteUserValidator
{
    public async Task<DeleteUserStatusDto> Validate(Domain.Entities.UserEntity.User? user)
    {
        if (user==null)
        {
            return DeleteUserStatusDto.NotFound;
        }

        return DeleteUserStatusDto.Success;
    }
}
public class LoginUserValidator
{
    public async Task<LoginUserStatusDto> Validate(LoginUserDto Model,Domain.Entities.UserEntity.User? user)
    {
        if (user==null)
            return LoginUserStatusDto.Failed;
        
        if (!SecretHasher.Verify(Model.Password, user.Password))
            return LoginUserStatusDto.Failed;

        return LoginUserStatusDto.Success;
    }
}
public class RegisterUserValidator(IUserRepository userRepository)
{
    public async Task<RegisterUserStatusDto> Validate(RegisterUserDto registerUserDto)
    {
        if (await userRepository.IsUserExistAsyncEmailOrMobile(registerUserDto.Email,registerUserDto.Phone))
        {
            return RegisterUserStatusDto.DuplicateInformation;
        }

        return RegisterUserStatusDto.Success;
    }
}