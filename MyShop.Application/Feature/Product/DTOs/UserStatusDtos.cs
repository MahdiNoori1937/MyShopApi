namespace MyShop.Application.Feature.User.DTOs;

public enum CreateProductStatusDto
{
    DuplicateInformation=400,
    Success=200,
    Failed=401
}
public enum UpdateProductStatusDto
{
    DuplicateInformation=400,
    Success=201,
    Failed=401,
    NotFound=402
}
public enum DeleteProductStatusDto
{
    Success=203,
    Failed=401,
    NotFound=402
}