namespace MyShop.Application.Feature.User.DTOs;

public enum CreateUserStatusDto
{
    DuplicateInformation=400,
    Success=200,
    Failed=401
}
public enum UpdateUserStatusDto
{
    DuplicateInformation=400,
    Success=201,
    Failed=401,
    NotFound=402
}
public enum DeleteUserStatusDto
{
    Success=203,
    Failed=401,
    NotFound=402
}

public enum LoginUserStatusDto
{
    Success=203,
    Failed=402,
}
public enum RegisterUserStatusDto
{
    DuplicateInformation=400,
    Success=200,
    Failed=401
}