using System.Runtime;
using MyShop.Application.Commonn.Dtos.DTOs.Shared;

namespace MyShop.Application.Feature.Product.DTOs;

public class CreateProductDto
{
    public string Name { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public string ManufactureEmail { get; set; }
    
    public string ManufacturePhone { get; set; }

    public int UserId { get; set; }
}

public class UpdateProductDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public string ManufactureEmail { get; set; }
    
    public string ManufacturePhone { get; set; }

    public int UserId { get; set; }
}

public class SearchProductDto:BasePaging<ProductListDto>
{
    public string? Search { get; set; }

    public int? Id { get; set; }
}

public class ProductListDto
{
    public string Name { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public string ManufactureEmail { get; set; }
    
    public string ManufacturePhone { get; set; }

    public DateTime CreateDate { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public string ManufactureEmail { get; set; }
    
    public string ManufacturePhone { get; set; }
}

public class DeleteProductDto
{
    public int UserId { get; set; } 
    
    public int  ProductId{ get; set; }
}