using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Entities.ProductEntity;

namespace MyShop.Data.Configurations.ProductConfig;

public class ProductConfig:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.Property(c=>c.CreateDate).IsRequired();
        builder.Property(c=>c.IsDelete).IsRequired();


        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.ManufactureEmail).IsRequired().HasMaxLength(50);
        builder.Property(c => c.ManufacturePhone).IsRequired().HasMaxLength(20);
        
        builder.HasIndex(c=> new {c.CreateDate , c.ManufactureEmail})
            .IsUnique();

        #region Relations

        builder.HasOne(c => c.User).WithMany(c => c.Products).HasForeignKey(c => c.UserId);


        #endregion
    }
}