using MediatR;
using MyShop.Application.Feature.User.DTOs;

namespace MyShop.Application.Feature.User.Command;

public class CreateUserCommand : IRequest<CreateUserStatusDto>
{
    public CreateUserDto UserDto { get; set; }

    public CreateUserCommand(CreateUserDto userDto)
    {
        UserDto = userDto;
    }
}
public class UpdateUserCommand : IRequest<UpdateUserStatusDto>
{
    public UpdateUserDto UserDto { get; set; }

    public UpdateUserCommand(UpdateUserDto userDto)
    {
        UserDto = userDto;
    }
}
public class DeleteUserCommand : IRequest<DeleteUserStatusDto>
{
    public int Userid { get; set; }

    public DeleteUserCommand(int userid)
    {
        Userid = userid;
    }
}