using MyShop.Domain.Entities.ProductEntity;
using MyShop.Domain.Interfaces.SharedInterface;

namespace MyShop.Domain.Interfaces.IProductInterface;

public interface IProductRepository:ISharedRepository<Product>
{
    
}