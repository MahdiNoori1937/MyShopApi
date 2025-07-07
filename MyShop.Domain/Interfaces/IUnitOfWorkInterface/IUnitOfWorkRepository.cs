namespace MyShop.Domain.Interfaces.IUnitOfWorkInterface;

public interface IUnitOfWorkRepository
{
    Task SaveChangesAsync();
}