using FluentValidation;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Application.Feature.User.Validators;

#region CreateUserDtoValidator

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(c => c.Email).EmailAddress().WithMessage("لطفا ایمیل به صورت صحیح وارد کنید")
            .MinimumLength(10).WithMessage("لطفا ایمیل بیشتر از 10 کاراکتر باشد")
            .MaximumLength(190).WithMessage("لطفا ایمیل کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.Phone).NotEmpty().WithMessage("لطفا شماره مبایل را وارد کنید")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره موبایل وارد شده معتبر نیست.");

        RuleFor(c => c.FullName).NotEmpty().WithMessage("لطفا نام خود را وارد کنید")
            .MinimumLength(3).WithMessage("لطفا نام بیشتر از 3 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا نام کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.Password).NotEmpty().WithMessage("لطفا رمز عبور خود را انتخاب کنید")
            .MinimumLength(4).WithMessage("لطفا رمز عبور بیشتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا رمز عبور کمتر از 50 کاراکتر باشد");
        
    }
}

#endregion

#region UpdateUserDtoValidator

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {

        RuleFor(c => c.Email).EmailAddress().WithMessage("لطفا ایمیل به صورت صحیح وارد کنید")
            .MinimumLength(10).WithMessage("لطفا ایمیل بیشتر از 10 کاراکتر باشد")
            .MaximumLength(190).WithMessage("لطفا ایمیل کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.Phone).NotEmpty().WithMessage("لطفا شماره مبایل را وارد کنید")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره موبایل وارد شده معتبر نیست.");

        RuleFor(c => c.FullName).NotEmpty().WithMessage("لطفا نام خود را وارد کنید")
            .MinimumLength(3).WithMessage("لطفا نام بیشتر از 3 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا نام کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.OldPassword).NotEmpty().WithMessage("لطفا رمز عبور خود را وارد کنید")
            .MinimumLength(4).WithMessage("لطفا رمز عبور بیشتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا رمز عبور کمتر از 50 کاراکتر باشد");
    }
}

#endregion
public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("لطفا ایمیل خود را وارد کنید")
            .EmailAddress().WithMessage("لطفا ایمیل به صورت صحیح وارد کنید")
            .MinimumLength(10).WithMessage("لطفا ایمیل بیشتر از 10 کاراکتر باشد")
            .MaximumLength(190).WithMessage("لطفا ایمیل کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("لطفا رمز عبور خود را وارد کنید")
            .MinimumLength(4).WithMessage("لطفا رمز عبور بیشتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا رمز عبور کمتر از 50 کاراکتر باشد");
    }
}

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("لطفا رمز عبور خود را وارد کنید")
            .EmailAddress().WithMessage("لطفا ایمیل به صورت صحیح وارد کنید")
            .MinimumLength(10).WithMessage("لطفا ایمیل بیشتر از 10 کاراکتر باشد")
            .MaximumLength(190).WithMessage("لطفا ایمیل کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.Phone).NotEmpty()
            .WithMessage("لطفا شماره مبایل را وارد کنید")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره موبایل وارد شده معتبر نیست.");

        RuleFor(c => c.FullName)
            .NotEmpty().WithMessage("لطفا نام خود را وارد کنید")
            .MinimumLength(3).WithMessage("لطفا نام بیشتر از 3 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا نام کمتر از 50 کاراکتر باشد");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("لطفا رمز عبور خود را انتخاب کنید")
            .MinimumLength(4).WithMessage("لطفا رمز عبور بیشتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("لطفا رمز عبور کمتر از 50 کاراکتر باشد");
    }
}