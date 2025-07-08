using MyShop.Data.Context;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;

namespace MyShop.Data.Repositories.UnitOfWorkRepository;

public class UnitOfWorkRepository(MyShopContext _db):IUnitOfWorkRepository
{
    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}