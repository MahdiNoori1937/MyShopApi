using MyShop.Application.Feature.Product.DTOs;

namespace MyShop.Application.Feature.Product.Validators.ProductValidateService;

public interface IProductValidatorService
{
    Task<DeleteProductStatusDto> DeleteValidate(Domain.Entities.ProductEntity.Product? product, int userId);
    Task<UpdateProductStatusDto> UpdateValidate(Domain.Entities.ProductEntity.Product? product, int userId);
    Task<CreateProductStatusDto> CreateValidate(CreateProductDto createProductDto);
}