using MyShop.Domain.Entities.Shared;
using MyShop.Domain.Entities.UserEntity;

namespace MyShop.Domain.Entities.ProductEntity;

public class Product:BaseEntity<int>
{
    public string Name { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public string ManufactureEmail { get; set; }
    
    public string ManufacturePhone { get; set; }

    #region Relations

    public int UserId { get; set; }

    public User? User { get; set; }
    

    #endregion
    
}