using AutoMapper;
using MyShop.Application.Feature.Product.DTOs;

namespace MyShop.Application.Feature.User.MappingProfiles;

public class ProductProfiles:Profile
{
    public ProductProfiles()
    {
        CreateMap<CreateProductDto, Domain.Entities.ProductEntity.Product>()
            .ForMember(set => set.CreateDate, opt
                => opt.MapFrom(src => DateTime.Now))
            .ForMember(set => set.IsDelete, opt => opt
                .MapFrom(src => false));

        CreateMap<UpdateProductDto, Domain.Entities.ProductEntity.Product>();
        CreateMap<Domain.Entities.ProductEntity.Product, ProductDto>();

    }
}