using MyShop.Application.Common.Security;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Application.Feature.User.Validators.UserValidateService;

public class UserValidateService(IUserRepository userRepository): IUserValidateService
{
    #region DeleteValidate

    public async Task<DeleteUserStatusDto> DeleteValidate(Domain.Entities.UserEntity.User? user)
    {
        if (user==null)
        {
            return DeleteUserStatusDto.NotFound;
            
        }

        return DeleteUserStatusDto.Success;
    }

    #endregion

    #region UpdateValidate

    public async Task<UpdateUserStatusDto> UpdateValidate(UpdateUserDto updateUserDto, Domain.Entities.UserEntity.User? user)
    {
        if (user==null)
            return UpdateUserStatusDto.NotFound;
        

        if (!SecretHasher.Verify(updateUserDto.OldPassword, user.Password))
            return UpdateUserStatusDto.NotFound;
        
        bool isEmailChanged = updateUserDto.Email != user.Email;
        bool isMobileChanged = updateUserDto.Phone != user.Phone;
        string? emailToCheck = isEmailChanged ? updateUserDto.Email : null;
        string? mobileToCheck = isMobileChanged ? updateUserDto.Phone : null;
        
        if ((isEmailChanged || isMobileChanged) &&
            await userRepository.IsUserExistAsyncEmailOrMobile(emailToCheck, mobileToCheck))
            return UpdateUserStatusDto.DuplicateInformation;
        

        return UpdateUserStatusDto.Success;
    }

    #endregion

    #region CreateValidate

    public async Task<CreateUserStatusDto> CreateValidate(CreateUserDto createUserDto)
    {
        if (await userRepository.IsUserExistAsyncEmailOrMobile(createUserDto.Email,createUserDto.Phone))
        {
            return CreateUserStatusDto.DuplicateInformation;
        }

        return CreateUserStatusDto.Success;
    }

    #endregion

    #region LoginValidate

    public async Task<LoginUserStatusDto> LoginValidate(LoginUserDto Model, Domain.Entities.UserEntity.User? user)
    {
        if (user==null)
            return LoginUserStatusDto.Failed;
        
        if (!SecretHasher.Verify(Model.Password, user.Password))
            return LoginUserStatusDto.Failed;

        return LoginUserStatusDto.Success;
    }

    #endregion

    #region RegisterValidate

    public async Task<RegisterUserStatusDto> RegisterValidate(RegisterUserDto registerUserDto)
    {
       
        if (await userRepository.IsUserExistAsyncEmailOrMobile(registerUserDto.Email,registerUserDto.Phone))
        {
            return RegisterUserStatusDto.DuplicateInformation;
        }

        return RegisterUserStatusDto.Success;
    }

    #endregion
}