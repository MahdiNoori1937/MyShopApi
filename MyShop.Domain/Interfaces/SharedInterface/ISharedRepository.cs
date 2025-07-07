namespace MyShop.Domain.Interfaces.SharedInterface;

public interface ISharedRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    
    Task AddAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(T entity);
    
    Task<bool> ExistsAsync(int id);
    
    IQueryable<T> Query();
    
}