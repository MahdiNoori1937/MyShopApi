namespace MyShop.Application.Feature.Product.DTOs;

public class CreateUserDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }
}

public class UpdateUserDto
{
    public int Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }
}

public class SearchUserDto
{
    public string? Search { get; set; }
}

public class UserListDto : SearchUserDto
{
    public int Id { get; set; }
    
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public int ProductCount { get; set; }
}