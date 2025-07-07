namespace LibraryApi.Domain.Interfaces.SharedInterface;

public interface ISharedRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    
    Task<bool> ExistsAsync(int id);
    
    
}