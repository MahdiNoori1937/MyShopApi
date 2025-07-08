namespace MyShop.Application.Feature.Product.DTOs;

public enum CreateProductStatusDto
{
    DuplicateInformation=410,
    Success=210,
    Failed=401
}
public enum UpdateProductStatusDto
{
    Success=211,
    DontHavePermission=413,
    NotFound=402
}
public enum DeleteProductStatusDto
{
    Success=212,
    Failed=411,
    NotFound=412,
    DontHavePermission=413,
}