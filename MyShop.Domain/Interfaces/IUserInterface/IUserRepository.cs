using MyShop.Domain.Entities.UserEntity;
using MyShop.Domain.Interfaces.SharedInterface;

namespace MyShop.Domain.Interfaces.IUserInterface;

public interface IUserRepository:ISharedRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    
    Task<bool> IsUserExistAsyncEmailOrMobile(string? email , string? mobile);
    
   
}