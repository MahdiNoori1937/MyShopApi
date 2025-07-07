using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Domain.Entities.UserEntity;

namespace MyShop.Data.Configurations.UserConfig;

public class UserConfig:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.Property(c=>c.CreateDate).IsRequired();
        builder.Property(c=>c.IsDelete).IsRequired();

        builder.Property(c => c.FullName).IsRequired().HasMaxLength(50);
        
        builder.Property(c => c.Email).IsRequired().HasMaxLength(50).IsUnicode();
        
        builder.Property(c => c.Phone).IsRequired().HasMaxLength(20).IsUnicode();
        
        builder.Property(c => c.Password).IsRequired().HasMaxLength(200);
        
        
    }
}