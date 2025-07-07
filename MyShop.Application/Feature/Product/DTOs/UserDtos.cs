namespace MyShop.Application.Feature.User.DTOs;

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
}

public class SearchProductDto
{
    public string? Search { get; set; }

    public int? Id { get; set; }
}

public class ProductListDto : SearchProductDto
{
    public string Name { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public string ManufactureEmail { get; set; }
    
    public string ManufacturePhone { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; }
}