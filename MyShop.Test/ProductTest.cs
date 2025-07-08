using System.Security.Cryptography;
using AutoMapper;
using Moq;
using MyShop.Application.Common.Interfaces;
using MyShop.Application.Feature.Product.Command;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.Product.Handler;
using MyShop.Application.Feature.Product.Validators;
using MyShop.Application.Feature.Product.Validators.ProductValidateService;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Domain.Entities.ProductEntity;
using MyShop.Domain.Interfaces.IProductInterface;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;
using Xunit;

namespace MyShop.Test;

public class ProductTest
{
    #region Constructor

    private readonly Mock<IProductRepository> _mockRepo;
    private readonly Mock<IUnitOfWorkRepository> _mockUnit;
    private readonly Mock<IProductValidatorService> _mockValidator;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IHttpContextService> _mockHttpContext;

    public ProductTest( )
    {
        _mockHttpContext = new Mock<IHttpContextService>( );
        _mockRepo = new Mock<IProductRepository>();
        _mockUnit = new Mock<IUnitOfWorkRepository>();
        _mockValidator = new Mock<IProductValidatorService>();
        _mockMapper = new Mock<IMapper>();
    }

    #endregion

    #region Handle_ShouldUpdateProduct_WhenValidationIsSuccessful

    [Fact]
    public async Task Handle_ShouldUpdateProduct_WhenValidationIsSuccessful()
    {
        UpdateProductDto dto = new()
        {
            Id = 2,
            Name = "ali",
            ManufactureEmail = "ali@ali.com",
            ManufacturePhone = "092141444",
            IsAvailable = true,
            UserId = 32
        };
        Product product = new()
        {
            Id = dto.Id,
            Name = dto.Name,
            IsAvailable = dto.IsAvailable,
            ManufactureEmail = dto.ManufactureEmail,
            ManufacturePhone = dto.ManufacturePhone,
            UserId = dto.UserId,
        };

        _mockRepo.Setup(m => m.GetByIdAsync(dto.Id)).ReturnsAsync(product);
        _mockValidator.Setup(c => c.UpdateValidate(product, dto.UserId)).ReturnsAsync(UpdateProductStatusDto.Success);
        _mockMapper.Setup(c=>c.Map<Product>(product)).Returns(product);
        _mockUnit.Setup(c=>c.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockHttpContext.Setup(c=>c.GetUserId()).Returns(dto.UserId);

        UpdateProductHandler handler = 
            new(_mockRepo.Object, _mockUnit.Object,_mockMapper.Object,_mockValidator.Object,_mockHttpContext.Object);
        UpdateProductCommand command = new(dto);
        
        UpdateProductStatusDto status =await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(UpdateProductStatusDto.Success, status);
        _mockValidator.Verify(c=>c.UpdateValidate(product,dto.UserId),Times.Once());
        _mockRepo.Verify(c=>c.UpdateAsync(It.IsAny<Product>()),Times.Once());
        _mockUnit.Verify(c=>c.SaveChangesAsync(),Times.Once());
        
        
    }

    #endregion

    #region Handle_ShouldNotUpdateProduct_WhenValidationFails

    [Fact]
    public async Task Handle_ShouldNotUpdateProduct_WhenValidationFails()
    {
        UpdateProductDto dto = new()
        {
            Id = 2,
            Name = "ali",
            ManufactureEmail = "ali@ali.com",
            ManufacturePhone = "092141444",
            IsAvailable = true,
            UserId = 32
        };
        Product product = new()
        {
            Id = dto.Id,
            Name = dto.Name,
            IsAvailable = dto.IsAvailable,
            ManufactureEmail = dto.ManufactureEmail,
            ManufacturePhone = dto.ManufacturePhone,
            UserId = dto.UserId,
        };

        _mockRepo.Setup(m => m.GetByIdAsync(dto.Id)).ReturnsAsync(product);
        _mockValidator.Setup(c => c.UpdateValidate(product, dto.UserId)).ReturnsAsync(UpdateProductStatusDto.Success);
        _mockMapper.Setup(c=>c.Map<Product>(product)).Returns(product);
        _mockUnit.Setup(c=>c.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockHttpContext.Setup(c=>c.GetUserId()).Returns(dto.UserId);
        UpdateProductHandler handler = 
            new(_mockRepo.Object, _mockUnit.Object,_mockMapper.Object,_mockValidator.Object,_mockHttpContext.Object);
        UpdateProductCommand command = new(dto);
        
        UpdateProductStatusDto status =await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(UpdateProductStatusDto.Success, status);
        _mockRepo.Verify(c=>c.UpdateAsync(It.IsAny<Product>()),Times.Once());
        _mockUnit.Verify(c=>c.SaveChangesAsync(),Times.Once());
        
        
    }

    #endregion

    #region Handle_ShouldCreateProduct_WhenValidationIsSuccessful

    [Fact]
    public async Task Handle_ShouldCreateProduct_WhenValidationIsSuccessful()
    {
        CreateProductDto createProductDto = new ()
        {
            Name = "Car",
            UserId = 5,
            IsAvailable = true,
            ManufactureEmail = "car@gmail.com",
            ManufacturePhone = "+9812345678"
        }; 
        Product product = new ()
        {
            Id = 15, 
            Name = createProductDto.Name,
            UserId = createProductDto.UserId,
            ManufactureEmail = createProductDto.ManufactureEmail,
            ManufacturePhone = createProductDto.ManufacturePhone,
            IsAvailable = createProductDto.IsAvailable
        };

        _mockValidator.Setup(v => v.CreateValidate(createProductDto)).ReturnsAsync(CreateProductStatusDto.Failed);
        _mockMapper.Setup(c=>c.Map<Product>(createProductDto)).Returns(product);
        _mockRepo.Setup(c=>c.AddAsync(product)).Returns(Task.CompletedTask);
        _mockUnit.Setup(c=>c.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockHttpContext.Setup(c=>c.GetUserId()).Returns(createProductDto.UserId);
        
        
        CreateProductHandler handler = new(_mockRepo.Object, _mockUnit.Object,_mockMapper.Object, _mockValidator.Object,_mockHttpContext.Object);
        CreateProductCommand createProductCommand = new (createProductDto);
        
        
        CreateProductStatusDto result = await handler.Handle(createProductCommand,CancellationToken.None);
        
        Assert.Equal(CreateProductStatusDto.Failed, result);
        _mockValidator.Verify(v => v.CreateValidate(createProductDto), Times.Once());
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never());
        _mockUnit.Verify(r => r.SaveChangesAsync(), Times.Never());
        
    }

    #endregion
    
    #region Handle_ShouldNotCreateProduct_WhenValidationFails

    [Fact]
    public async Task Handle_ShouldNotCreateProduct_WhenValidationFails()
    {
        CreateProductDto createProductDto = new ()
        {
            Name = "Tablet",
            UserId = 2,
            ManufactureEmail = "tablet@example.com",
            ManufacturePhone = "+9812345678",
            IsAvailable = true
        }; 
        Product product = new ()
        {
            Id = 15, 
            Name = createProductDto.Name,
            UserId = createProductDto.UserId,
            ManufactureEmail = createProductDto.ManufactureEmail,
        };
     

        _mockValidator.Setup(v => v.CreateValidate(createProductDto)).ReturnsAsync(CreateProductStatusDto.Failed);
        _mockMapper.Setup(c=>c.Map<Product>(createProductDto)).Returns(product);
        _mockRepo.Setup(r => r.AddAsync(product)).Returns(Task.CompletedTask);
        _mockUnit.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockHttpContext.Setup(c=>c.GetUserId()).Returns(createProductDto.UserId);
        
        CreateProductHandler handler = new(_mockRepo.Object, _mockUnit.Object,_mockMapper.Object, _mockValidator.Object,_mockHttpContext.Object);
        CreateProductCommand createProductCommand = new (createProductDto);
        
        CreateProductStatusDto result = await handler.Handle(createProductCommand,CancellationToken.None);
        
        Assert.Equal(CreateProductStatusDto.Failed, result);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never());
        _mockUnit.Verify(r => r.SaveChangesAsync(), Times.Never());
        
    }

    #endregion

    #region Handle_ShouldDeleteProduct_WhenValidationIsSuccessful

    [Fact]
    public async Task Handle_ShouldDeleteProduct_WhenValidationIsSuccessful()
    {
        Product product = new ()
        {
            Id = 1,
            Name = "Car",
            UserId = 5,
            CreateDate = DateTime.Now,
            IsAvailable = true,
            IsDelete = false,
            ManufactureEmail = "car@gmail.com",
            ManufacturePhone = "+9812345678"
        };
        

        _mockRepo.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
        _mockValidator.Setup(v => v.DeleteValidate(product, product.UserId)).ReturnsAsync(DeleteProductStatusDto.Success);
        _mockHttpContext.Setup(c=>c.GetUserId()).Returns(product.UserId);
        DeleteProductHandler handler = new(_mockRepo.Object, _mockUnit.Object, _mockValidator.Object,_mockHttpContext.Object);

        DeleteProductDto dto = new ()
        {
            ProductId = product.Id, 
            UserId = product.UserId 
        };
        DeleteProductCommand command = new(dto);

        
        DeleteProductStatusDto result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(DeleteProductStatusDto.Success, result);
        _mockRepo.Verify(r => r.DeleteAsync(product), Times.Once());
        _mockUnit.Verify(u => u.SaveChangesAsync(), Times.Once());
    }

    #endregion

    #region Handle_ShouldNotDeleteProduct_WhenValidationFails

    [Fact]
    public async Task Handle_ShouldNotDeleteProduct_WhenValidationFails()
    {
        Product product = new()
        {
            Id = 1,
            Name = "Car",
            UserId = 5
        };
        

        _mockRepo.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
        _mockValidator.Setup(v => v.DeleteValidate(product, product.UserId)).ReturnsAsync(DeleteProductStatusDto.Failed);
        _mockHttpContext.Setup(c=>c.GetUserId()).Returns(product.UserId);
        DeleteProductHandler handler = new(_mockRepo.Object, _mockUnit.Object, _mockValidator.Object,_mockHttpContext.Object);

        DeleteProductDto dto = new ()
        {
            ProductId = product.Id, 
            UserId = product.UserId
        };
        DeleteProductCommand command = new(dto);

        DeleteProductStatusDto result = await handler.Handle(command, CancellationToken.None);

     
        Assert.Equal(DeleteProductStatusDto.Failed, result);
        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Product>()), Times.Never());
        _mockUnit.Verify(u => u.SaveChangesAsync(), Times.Never());
    }

    #endregion

}