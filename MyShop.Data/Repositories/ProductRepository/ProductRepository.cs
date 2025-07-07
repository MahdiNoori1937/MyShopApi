using Microsoft.EntityFrameworkCore;
using MyShop.Data.Context;
using MyShop.Domain.Entities.ProductEntity;
using MyShop.Domain.Interfaces.IProductInterface;

namespace MyShop.Data.Repositories.ProductRepository;

public class ProductRepository(MyShopContext _db):IProductRepository
{
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task AddAsync(Product entity)
    {
        await _db.Products.AddAsync(entity);
    }

    public async Task UpdateAsync(Product entity)
    {
       _db.Products.Update(entity);
    }

    public async Task DeleteAsync(Product entity)
    {
         _db.Products.Remove(entity);
    }

    public async Task<bool> ExistsAsync(int id)
    {
       return await _db.Products.AnyAsync(c=>c.Id==id);
    }

    public IQueryable<Product> Query()
    {
        return _db.Products
            .Where(c=>!c.IsDelete)
            .OrderBy(c=>c.CreateDate)
            .Include(c=>c.User)
            .AsQueryable();
    }

    public async Task<bool> IsUniqueEmailAsync(string email, DateTime CreateDate)
    {
        return await _db.Products.AnyAsync(c=>c.ManufactureEmail==email || c.CreateDate==CreateDate);
    }
}