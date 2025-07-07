using FluentValidation;
using FluentValidation.Results;
using LibraryApi.Application.Common.Messages;
using LibraryApi.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Web.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public abstract class ApiBaseController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly StatusMessageProvider _responseMessage;

    protected ApiBaseController(IMediator mediator, StatusMessageProvider responseMessage)
    {
        _mediator = mediator;
        _responseMessage = responseMessage;
    }

    protected IActionResult OkResponse<T>(T data, string message = "عملیات موفقیت‌آمیز بود")
    {
        return Ok(ApiResponse<T>.Success(message, data));
    }

    protected IActionResult OkResponseNoData(Enum? Status=null,int? Code = null)
    {
        if (Code == null)
            Code = Convert.ToInt32(Status);
        
        string? message = _responseMessage.GetMessage(Code.Value);
        return Ok(ApiResponseNoData.Success(message));
    }

    protected IActionResult BadRequestResponse(Enum? Status=null,int? Code = null)
    {
        if (Code == null)
            Code = Convert.ToInt32(Status);
        string? message = _responseMessage.GetMessage(Code.Value);
        return BadRequest(ApiResponseNoData.Failed(message));
    }

    protected IActionResult BadRequestValidation(List<FluentValidation.Results.ValidationFailure> errors)
    {
        string firstError = errors.FirstOrDefault()?.ErrorMessage ?? "خطای اعتبارسنجی رخ داد";
        return BadRequest(ApiResponseNoData.Failed(firstError));
    }

    protected async Task<IActionResult?> HandleValidationAsync<T>(IValidator<T> validator, T model)
    {
        ValidationResult validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
            return BadRequestValidation(validationResult.Errors);

        
        return null; 
    }
}
