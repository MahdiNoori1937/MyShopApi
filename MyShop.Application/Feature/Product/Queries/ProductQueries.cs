using Azure.Core;
using MediatR;
using MyShop.Application.Feature.Product.DTOs;

namespace MyShop.Application.Feature.Product.Queries;

public class ListProductQueries : IRequest<SearchProductDto>
{
    public SearchProductDto SearchProductDto { get; set; }

    public ListProductQueries(SearchProductDto searchProductDto)
    {
        SearchProductDto = searchProductDto;
    }
}

public class GetProductQueries : IRequest<ProductDto>
{
    public int Id { get; set; }

    public GetProductQueries(int id)
    {
        Id = id;
    }
}