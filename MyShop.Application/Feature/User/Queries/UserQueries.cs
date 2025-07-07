using MediatR;
using MyShop.Application.Feature.User.DTOs;

namespace MyShop.Application.Feature.User.Queries;

public class ListUserQueries : IRequest<SearchUserDto>
{
    public SearchUserDto SearchUserDto { get; set; }

    public ListUserQueries(SearchUserDto searchUserDto)
    {
        SearchUserDto = searchUserDto;
    }
}

public class GetUserQueries : IRequest<UserDto>
{
    public int UserId { get; set; }

    public GetUserQueries(int userId)
    {
        UserId = userId;
    }
}

public class LoginUserQueries : IRequest<LoginUserStatusDtoClass>
{
    public LoginUserDto LoginUserDto { get; set; }

    public LoginUserQueries(LoginUserDto loginUserDto)
    {
        LoginUserDto = loginUserDto;
    }
}
