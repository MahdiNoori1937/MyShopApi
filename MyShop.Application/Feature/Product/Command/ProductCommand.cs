using MediatR;
using MyShop.Application.Feature.Product.DTOs;

namespace MyShop.Application.Feature.Product.Command;

public class CreateProductCommand : IRequest<CreateProductStatusDto>
{
    public CreateProductDto ProductDto { get; set; }

    public CreateProductCommand(CreateProductDto productDto)
    {
        ProductDto = productDto;
    }
}
public class UpdateProductCommand : IRequest<UpdateProductStatusDto>
{
    public UpdateProductDto ProductDto { get; set; }

    public UpdateProductCommand(UpdateProductDto productDto)
    {
        ProductDto = productDto;
    }
}
public class DeleteProductCommand : IRequest<DeleteProductStatusDto>
{
    public DeleteProductDto ProductDto { get; set; }

    public DeleteProductCommand(DeleteProductDto productDto)
    {
        ProductDto = productDto;
    }
}