using Microsoft.Extensions.DependencyInjection;
using MyShop.Application.Feature.Product.Validators;
using MyShop.Application.Feature.User.Validators;
using MyShop.Data.Repositories.ProductRepository;
using MyShop.Data.Repositories.UserRepository;
using MyShop.Domain.Interfaces.IProductInterface;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.IOC.DependencyInjection;

public static class ShopIOC
{
    public static void IOC(this IServiceCollection services)
    {
        #region Repository

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        #endregion

        #region Validators

        services.AddScoped<CreateProductValidator>();
        services.AddScoped<UpdateProductValidator>();  
        services.AddScoped<DeleteProductValidator>();  
        
        services.AddScoped<CreateUserValidator>();
        services.AddScoped<UpdateUserValidator>();
        services.AddScoped<DeleteUserValidator>();
        services.AddScoped<LoginUserValidator>();
        services.AddScoped<RegisterUserValidator>();

        #endregion
    }
}