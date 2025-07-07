namespace MyShop.Application.Feature.Product.DTOs;

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