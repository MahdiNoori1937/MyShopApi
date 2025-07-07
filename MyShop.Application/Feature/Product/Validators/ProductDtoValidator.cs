using FluentValidation;
using MyShop.Application.Feature.Product.DTOs;


namespace MyShop.Application.Feature.Product.Validators;

#region CreateProductDtoValidator

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithMessage("لطفا شناسه کاربری را ارسال کنید");

        RuleFor(c => c.ManufactureEmail).NotEmpty().WithMessage("لطفا ایمیل محصول را وارد کنید")
            .EmailAddress().WithMessage("لطفا ایمیل به صورت صحیح وارد کنید")
            .MinimumLength(10).WithMessage("لطفا ایمیل بیشتر از 10 کاراکتر باشد")
            .MaximumLength(190).WithMessage("لطفا ایمیل کمتر از 50 کاراکتر باشد");
        
        RuleFor(c=>c.ManufacturePhone).NotEmpty().WithMessage("لطفا شماره مبایل را وارد کنید")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره موبایل وارد شده معتبر نیست.");
        
        RuleFor(c=>c.Name).NotEmpty().WithMessage("لطفا نام محصول را وارد کنید")
            .MinimumLength(3).WithMessage("لطفا نام بیشتر از 3 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا نام کمتر از 50 کاراکتر باشد");
        
    }
}

#endregion

#region UpdateProductDtoValidator

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("لطفا شناسه کاربری را ارسال کنید");
        
        RuleFor(c => c.ManufactureEmail).EmailAddress().WithMessage("لطفا ایمیل به صورت صحیح وارد کنید")
            .MinimumLength(10).WithMessage("لطفا ایمیل بیشتر از 10 کاراکتر باشد")
            .MaximumLength(190).WithMessage("لطفا ایمیل کمتر از 50 کاراکتر باشد");
        
        RuleFor(c=>c.ManufacturePhone).NotEmpty().WithMessage("لطفا شماره مبایل را وارد کنید")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره موبایل وارد شده معتبر نیست.");
        
        RuleFor(c=>c.Name).NotEmpty().WithMessage("لطفا نام خود را وارد کنید")
            .MinimumLength(3).WithMessage("لطفا نام بیشتر از 3 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا نام کمتر از 50 کاراکتر باشد");
        
       
    }
}

#endregion