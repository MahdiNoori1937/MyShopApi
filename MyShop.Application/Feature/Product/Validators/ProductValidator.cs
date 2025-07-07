using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Domain.Interfaces.IProductInterface;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Application.Feature.Product.Validators;

public class CreateProductValidator(IProductRepository productRepository)
{
    public async Task<CreateProductStatusDto> Validate(CreateProductDto createProductDto)
    {
        if (await productRepository.IsUniqueEmailAsync(createProductDto.ManufactureEmail,DateTime.Now))
        {
            return CreateProductStatusDto.DuplicateInformation;
        }

        return CreateProductStatusDto.Success;
    }
}
public class UpdateProductValidator
{
    public async Task<UpdateProductStatusDto> Validate(Domain.Entities.ProductEntity.Product? product,int userId )
    {
        if (product==null)
            return UpdateProductStatusDto.NotFound;
        if (product.UserId != userId)
            return UpdateProductStatusDto.DontHavePermission;
        
        return UpdateProductStatusDto.Success;
    }
}

public class DeleteProductValidator
{
    public async Task<DeleteProductStatusDto> Validate(Domain.Entities.ProductEntity.Product? product,int userId )
    {
        if (product==null)
            return DeleteProductStatusDto.NotFound;
        
        if (product.UserId != userId)
            return DeleteProductStatusDto.DontHavePermission;
        
        return DeleteProductStatusDto.Success;
    }
}