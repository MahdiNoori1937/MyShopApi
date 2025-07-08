using AutoMapper;
using MediatR;
using MyShop.Application.Common.Interfaces;
using MyShop.Application.Feature.Product.Command;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.Product.Queries;
using MyShop.Application.Feature.Product.Validators;
using MyShop.Application.Feature.Product.Validators.ProductValidateService;
using MyShop.Domain.Interfaces.IProductInterface;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;

namespace MyShop.Application.Feature.Product.Handler;

#region CreateProductHandler

public class CreateProductHandler(
    IProductRepository productRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IMapper mapper,
    IProductValidatorService validator,
    IHttpContextService httpContextService)
    : IRequestHandler<CreateProductCommand, CreateProductStatusDto>
{
    public async Task<CreateProductStatusDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        request.ProductDto.UserId= httpContextService.GetUserId();
        CreateProductStatusDto status = await validator.CreateValidate(request.ProductDto);
        if (status != CreateProductStatusDto.Success)
            return status;
        
        Domain.Entities.ProductEntity.Product? product = mapper.Map<Domain.Entities.ProductEntity.Product>(request.ProductDto);

        
        await productRepository.AddAsync(product);
        await unitOfWorkRepository.SaveChangesAsync();
        if (product.Id == 0)
            return CreateProductStatusDto.Failed;
        return CreateProductStatusDto.Success;
    }
}

#endregion

#region UpdateProductHandler

public class UpdateProductHandler(
    IProductRepository productRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IMapper mapper,
    IProductValidatorService validator,
    IHttpContextService httpContextService)
    : IRequestHandler<UpdateProductCommand, UpdateProductStatusDto>
{
    public async Task<UpdateProductStatusDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.ProductEntity.Product product = await productRepository.GetByIdAsync(request.ProductDto.Id)?? new();
        
        request.ProductDto.UserId=httpContextService.GetUserId();
        UpdateProductStatusDto status = await validator.UpdateValidate(product,request.ProductDto.UserId);
        if (status != UpdateProductStatusDto.Success)
            return status;
        
        Domain.Entities.ProductEntity.Product? productMapper  = mapper.Map(request.ProductDto,product);

        
        await productRepository.UpdateAsync(productMapper);
        await unitOfWorkRepository.SaveChangesAsync();
        return UpdateProductStatusDto.Success;
    }
}

#endregion

#region DeleteProductHandler

public class DeleteProductHandler(
    IProductRepository productRepository,
    IUnitOfWorkRepository unitOfWorkRepository,
    IProductValidatorService validator,
    IHttpContextService httpContextService)
    : IRequestHandler<DeleteProductCommand, DeleteProductStatusDto>
{
    public async Task<DeleteProductStatusDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        request.ProductDto.UserId= httpContextService.GetUserId();
        Domain.Entities.ProductEntity.Product product = await productRepository.GetByIdAsync(request.ProductDto.ProductId)?? new();
       
        request.ProductDto.UserId=httpContextService.GetUserId();
        DeleteProductStatusDto status = await validator.DeleteValidate(product,request.ProductDto.UserId);
        if (status != DeleteProductStatusDto.Success)
            return status;
       
        await productRepository.DeleteAsync(product);
        await unitOfWorkRepository.SaveChangesAsync();
        return DeleteProductStatusDto.Success;
    }
}

#endregion

#region GetProductHandler

public class GetProductHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductQueries, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductQueries request, CancellationToken cancellationToken)
    {
        return mapper.Map<ProductDto>(await productRepository.GetByIdAsync(request.Id));
    }
}

#endregion

#region ListProductHandler

public class ListProductHandler(IProductRepository productRepository)
    : IRequestHandler<ListProductQueries, SearchProductDto>
{

    public async Task<SearchProductDto> Handle(ListProductQueries request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.ProductEntity.Product> query =  productRepository.Query();

        if (!string.IsNullOrEmpty(request.SearchProductDto.Search))
        {
            string search = request.SearchProductDto.Search.Trim().ToLower();
            
            query = query.Where(p => p.Name.ToLower().Trim().Contains(search));
            
        }
        bool idNotNull = request.SearchProductDto.Id!=null && request.SearchProductDto.Id>0;
        
        if (idNotNull)
        {
            query = query.Where(c=>c.UserId==request.SearchProductDto.Id);
        }

        await request.SearchProductDto.Paging(query.Select(p => new ProductListDto
        {
            Name = p.Name,
            IsAvailable = p.IsAvailable,
            ManufactureEmail = p.ManufactureEmail,
            ManufacturePhone = p.ManufacturePhone,
            CreateDate = p.CreateDate,
            UserId = p.UserId,
            FullName = p.User!.FullName,
        }));

        return request.SearchProductDto;



    }
}

#endregion