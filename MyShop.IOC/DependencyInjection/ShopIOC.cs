using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Application.Common.Interfaces;
using MyShop.Application.Common.Messages;
using MyShop.Application.Feature.Product.Validators.ProductValidateService;
using MyShop.Application.Feature.User.Validators.UserValidateService;
using MyShop.Data.Authentication.Jwt;
using MyShop.Data.Repositories.ProductRepository;
using MyShop.Data.Repositories.UnitOfWorkRepository;
using MyShop.Data.Repositories.UserRepository;
using MyShop.Domain.Interfaces.IJwtInterface;
using MyShop.Domain.Interfaces.IProductInterface;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.IOC.DependencyInjection;

public static class ShopIOC
{
    public static void IOC(this IServiceCollection services)
    {
        #region Repository

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

        #endregion

        #region Validators

        services.AddScoped<IProductValidatorService,ProductValidateService>();
        services.AddScoped<IUserValidateService,UserValidateService>();
        services.AddScoped<IJwtService,JwtService>();
    
        
      

        services.AddScoped<StatusMessageProvider>();

        #endregion

    }
}
