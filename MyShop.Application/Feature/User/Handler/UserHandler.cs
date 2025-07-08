
using AutoMapper;
using MediatR;
using MyShop.Application.Commonn.Security;
using MyShop.Application.Feature.User.Command;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Application.Feature.User.Queries;
using MyShop.Application.Feature.User.Validators;
using MyShop.Application.Feature.User.Validators.UserValidateService;
using MyShop.Domain.Common;
using MyShop.Domain.Interfaces.IJwtInterface;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Application.Feature.User.Handler;

public class CreateUserHandler(
    IUserRepository userRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IMapper mapper,
    IUserValidateService validator)
    : IRequestHandler<CreateUserCommand, CreateUserStatusDto>
{
    public async Task<CreateUserStatusDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        CreateUserStatusDto status = await validator.CreateValidate(request.UserDto);
        if (status != CreateUserStatusDto.Success)
            return status;

        Domain.Entities.UserEntity.User user = mapper.Map<Domain.Entities.UserEntity.User>(request.UserDto);


        await userRepository.AddAsync(user);
        await unitOfWorkRepository.SaveChangesAsync();
        if (user.Id == null)
            return CreateUserStatusDto.Failed;
        return CreateUserStatusDto.Success;
    }
}

public class UpdateUserHandler(
    IUserRepository userRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IMapper mapper,
    IUserValidateService validator)
    : IRequestHandler<UpdateUserCommand, UpdateUserStatusDto>
{
    public async Task<UpdateUserStatusDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.UserEntity.User user = await userRepository.GetByIdAsync(request.UserDto.Id) ?? new();

        UpdateUserStatusDto status = await validator.UpdateValidate(request.UserDto, user);
        if (status != UpdateUserStatusDto.Success)
            return status;

        Domain.Entities.UserEntity.User? userMapper = mapper.Map(request.UserDto, user);

        if (request.UserDto.NewPassword != null)
        {
            userMapper.Password = SecretHasher.Hash(request.UserDto.NewPassword);
        }
        
        
        await userRepository.UpdateAsync(userMapper);
        await unitOfWorkRepository.SaveChangesAsync();
        return UpdateUserStatusDto.Success;
    }
}

public class DeleteUserHandler(
    IUserRepository userRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IUserValidateService validator)
    : IRequestHandler<DeleteUserCommand, DeleteUserStatusDto>
{
    public async Task<DeleteUserStatusDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.UserEntity.User user = await userRepository.GetByIdAsync(request.Userid) ?? new();

        DeleteUserStatusDto status = await validator.DeleteValidate(user);
        if (status != DeleteUserStatusDto.Success)
            return status;

        user.IsDelete = true;

        await userRepository.UpdateAsync(user);
        await unitOfWorkRepository.SaveChangesAsync();
        return DeleteUserStatusDto.Success;
    }
}

public class GetUserHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserQueries, UserDto>
{
    public async Task<UserDto> Handle(GetUserQueries request, CancellationToken cancellationToken)
    {
        return mapper.Map<UserDto>(await userRepository.GetByIdAsync(request.UserId));
    }
}

public class ListUserHandler(IUserRepository userRepository)
    : IRequestHandler<ListUserQueries, SearchUserDto>
{
    public async Task<SearchUserDto> Handle(ListUserQueries request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.UserEntity.User> query = userRepository.Query();

        if (!string.IsNullOrEmpty(request.SearchUserDto.Search))
        {
            string search = request.SearchUserDto.Search.Trim().ToLower();
            
            query=query.Where(c=>c.FullName.ToLower().Trim().Contains(search)
            || c.Email.ToLower().Trim().Contains(search)
            || c.FullName.ToLower().Trim().Contains(search));
        }

        await request.SearchUserDto.Paging(query.Select(u => new ListUserDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            Phone = u.Phone,
            ProductCount = u.Products.Count,
            CreatDate = u.CreateDate
        }));
        return request.SearchUserDto;
    }
}

public class LoginUserHandler:IRequestHandler<LoginUserQueries,LoginUserStatusDtoClass>
{
    private readonly IUserRepository userRepository;
    private readonly IJwtService _jwtService;
    private readonly LoginUserValidator validator;
    
    
    public async Task<LoginUserStatusDtoClass> Handle(LoginUserQueries request, CancellationToken cancellationToken)
    {
        Domain.Entities.UserEntity.User user = await userRepository.GetByEmailAsync(request.LoginUserDto.Email)?? new();
        
        LoginUserStatusDto status =await validator.Validate(request.LoginUserDto, user);

        if (status != LoginUserStatusDto.Success)
            return new LoginUserStatusDtoClass
            {
                LoginUserStatusDto = status,
                Token = null
            };
        
        string token = await _jwtService.GetJwtTokenAsync(new ClaimSetDto()
        {
            Id = user.Id,
        });

        return new LoginUserStatusDtoClass
        {
            LoginUserStatusDto = status,
            Token = token
        };

    }
}
public class RegisterUserHandler(
    IUserRepository userRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IMapper mapper,
    RegisterUserValidator validator)
    : IRequestHandler<RegisterUserCommand, RegisterUserStatusDto>
{
    public async Task<RegisterUserStatusDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        RegisterUserStatusDto status = await validator.Validate(request.RegisterUserDto);
        if (status != RegisterUserStatusDto.Success)
            return status;

        Domain.Entities.UserEntity.User user = mapper.Map<Domain.Entities.UserEntity.User>(request.RegisterUserDto);


        await userRepository.AddAsync(user);
        await unitOfWorkRepository.SaveChangesAsync();
        if (user.Id != null)
            return RegisterUserStatusDto.Failed;
        return RegisterUserStatusDto.Success;
    }
}