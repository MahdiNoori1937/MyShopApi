using MyShop.Application.Commonn.Dtos.DTOs.Shared;

namespace MyShop.Application.Feature.User.DTOs;

public class CreateUserDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }
}

public class UpdateUserDto
{
    public int Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string OldPassword { get; set; }
    
    public string NewPassword { get; set; }
}

public class SearchUserDto:BasePaging<ListUserDto>
{
    public string? Search { get; set; }
}

public class ListUserDto 
{
    public int Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public int ProductCount { get; set; }

    public DateTime CreatDate { get; set; }

    
}
public class UserDto
{
    public int Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    
    public DateTime CreatDate { get; set; }
}

public class LoginUserDto
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}


public class RegisterUserDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }
}
public class LoginUserStatusDtoClass
{
    public LoginUserStatusDto LoginUserStatusDto { get; set; }

    public string? Token { get; set; }
}

public class ClaimUserDto
{
    public int Id { get; set; }
}