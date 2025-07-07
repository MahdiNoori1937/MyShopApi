using MyShop.Domain.Entities.ProductEntity;
using MyShop.Domain.Entities.Shared;

namespace MyShop.Domain.Entities.UserEntity;

public class User:BaseEntity<int>
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }

    #region Relations

    public ICollection<Product>? Products { get; set; }

    #endregion
}