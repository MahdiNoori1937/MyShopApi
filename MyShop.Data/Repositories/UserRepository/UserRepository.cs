using Microsoft.EntityFrameworkCore;
using MyShop.Data.Context;
using MyShop.Domain.Entities.UserEntity;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Data.Repositories.UserRepository;

public class UserRepository(MyShopContext _db): IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task AddAsync(User entity)
    {
       await _db.Users.AddAsync(entity);
    }

    public async Task UpdateAsync(User entity)
    {
        _db.Users.Update(entity);
    }

    public async Task DeleteAsync(User entity)
    {
         _db.Users.Remove(entity);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Users.AnyAsync(c => c.Id == id);
    }

    public IQueryable<User> Query()
    {
        return _db.Users
            .Where(c=>!c.IsDelete)
            .Include(c=>c.Products)
            .OrderBy(c=>c.CreateDate)
            .AsQueryable();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(c => c.Email == email && !c.IsDelete);
    }

    public async Task<bool> IsUserExistAsyncEmailOrMobile(string? email, string? mobile)
    {
        return await _db.Users.AnyAsync(c=>c.Email==email || c.Phone==mobile);

    }
}