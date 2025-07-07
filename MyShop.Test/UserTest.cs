using System.Security.Cryptography;
using Moq;
using MyShop.Application.Feature.Product.Command;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.Product.Handler;
using MyShop.Application.Feature.Product.Validators;
using MyShop.Domain.Entities.ProductEntity;
using MyShop.Domain.Interfaces.IProductInterface;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;
using Xunit;

namespace MyShop.Test;

public class TestDeleteUser
{
    public async Task Handle_ShouldDeleteProduct_WhenValidationIsSuccessful()
    {
        var product = new Product
        {
            Id = 1,
            CreateDate = DateTime.Now,
            IsDelete = false,
            Name = "Car",
            IsAvailable = true,
            ManufactureEmail = "car@gmail.com",
            ManufacturePhone = "+9812345678",
            UserId = 5,
        };
        var MockRep = new Mock<IProductRepository>();
        var MockUnit = new Mock<IUnitOfWorkRepository>();
        var MockValidat = new Mock<DeleteProductValidator>();
        
        MockRep.Setup(r=>r.GetByIdAsync(product.Id)).ReturnsAsync(product);
        MockValidat.Setup(v => v.Validate(product, product.UserId)).ReturnsAsync(DeleteProductStatusDto.Success);
        
        var handler = new DeleteProductHandler(MockRep.Object, MockUnit.Object, MockValidat.Object);

        DeleteProductDto dto = new()
        {
            UserId = product.UserId,
            ProductId = product.Id
        };
        var command = new DeleteProductCommand(dto);
        
        var result = await handler.Handle(command, default);
        
        Assert.Equal(DeleteProductStatusDto.Success, result);
        MockRep.Verify(r => r.DeleteAsync(It.IsAny<Product>()), Times.Never);
        MockUnit.Verify(u => u.SaveChangesAsync(), Times.Never);



    }
}