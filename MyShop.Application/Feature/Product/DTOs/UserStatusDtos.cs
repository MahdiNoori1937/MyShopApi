namespace MyShop.Application.Feature.Product.DTOs;

public enum CreateProductStatusDto
{
    DuplicateInformation=400,
    Success=200,
    Failed=401
}
public enum UpdateProductStatusDto
{
    Success=201,
    Failed=401,
    DontHavePermission=402,
    NotFound=402
}
public enum DeleteProductStatusDto
{
    Success=203,
    Failed=401,
    NotFound=402,
    DontHavePermission=402,
}