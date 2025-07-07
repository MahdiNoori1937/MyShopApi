namespace MyShop.Domain.Entities.Shared;

public class BaseEntity<T>
{
    public T Id { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }
}