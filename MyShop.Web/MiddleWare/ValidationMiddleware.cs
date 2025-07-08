namespace MyShop.Web.MiddleWare;

public class ValidationMiddleware
{
    public readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await  _next(context);

        }
        catch (FluentValidation.ValidationException Error)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            var errors = Error.Errors.Select(c => new
            {
                Filed = c.PropertyName,
                ErrorMessage = c.ErrorMessage
            });
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "خطای اعتبارسنجی رخ داده است",
                Errors = errors
            });
        }
    }
}