using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyShop.Domain.Entities.ProductEntity;
using MyShop.Domain.Entities.UserEntity;

namespace MyShop.Data.Context;

public class MyShopContext:DbContext
{
    public MyShopContext(DbContextOptions<MyShopContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.GetForeignKeys()
                .Where(c=>!c.IsOwnership && c.DeleteBehavior==DeleteBehavior.Cascade)
                .ToList()
                .ForEach(c=>c.DeleteBehavior=DeleteBehavior.Restrict);
        }
    }
}