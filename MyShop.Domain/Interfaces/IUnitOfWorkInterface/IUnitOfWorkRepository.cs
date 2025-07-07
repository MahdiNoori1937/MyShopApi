namespace LibraryApi.Domain.Interfaces.IUnitOfWorkInterface;

public interface IUnitOfWorkRepository
{
    Task SaveChangesAsync();
}