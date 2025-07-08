using MyShop.Application.Feature.Product.DTOs;
using MyShop.Domain.Interfaces.IProductInterface;

namespace MyShop.Application.Feature.Product.Validators.ProductValidateService;

public class ProductValidateService(IProductRepository productRepository):IProductValidatorService
{
    public async Task<DeleteProductStatusDto> DeleteValidate(Domain.Entities.ProductEntity.Product? product, int userId)
    {
        if (product==null)
            return DeleteProductStatusDto.NotFound;
        
        if (product.UserId != userId)
            return DeleteProductStatusDto.DontHavePermission;
        
        return DeleteProductStatusDto.Success;
    }

    public async Task<UpdateProductStatusDto> UpdateValidate(Domain.Entities.ProductEntity.Product? product, int userId)
    {
        if (product==null)
            return UpdateProductStatusDto.NotFound;
        if (product.UserId != userId)
            return UpdateProductStatusDto.DontHavePermission;
        
        return UpdateProductStatusDto.Success;
    }

    public async Task<CreateProductStatusDto> CreateValidate(CreateProductDto createProductDto)
    {
        if (await productRepository.IsUniqueEmailAsync(createProductDto.ManufactureEmail,DateTime.Now))
        {
            return CreateProductStatusDto.DuplicateInformation;
        }

        return CreateProductStatusDto.Success;
    }
}