using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Commonn.Messages;
using MyShop.Application.Extensions;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.Product.Queries;
using MyShop.Application.Feature.User.Command;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Application.Feature.User.Queries;
using MyShop.Application.Feature.User.Validators;
using MyShop.Domain.Interfaces.IUserInterface;
using MyShop.Web.Extensions;
using MyShop.Web.Filters.Permisions;

namespace MyShop.Web.Controllers;

public class UserController(IMediator mediator, StatusMessageProvider responseMessage,IUserRepository userRepository)
    : ApiBaseController(mediator, responseMessage)
{
    #region GetAll

    [HttpGet]
    public async Task<IActionResult> GetAll([FromBody] SearchUserDto request)
    {
        SearchUserDto Model = await _mediator.Send(new ListUserQueries(request));
        if (Model.Entities.IsNullOrEmpty())
            return BadRequestResponse(null, 404);

        return OkResponse(Model);
    }

    #endregion

    #region GetById

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        UserDto model = await _mediator.Send(new GetUserQueries(id));
        if (model.ModelIsNull())
            return BadRequestResponse(null, 400);

        return OkResponse(model);
    }

    #endregion

    #region Create

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto request)
    {
        IActionResult? Validation = await HandleValidationAsync(new CreateUserDtoValidator(userRepository), request);
        if (Validation is not null)
            return Validation;

        CreateUserStatusDto status = await _mediator.Send(new CreateUserCommand(request));
        if (status != CreateUserStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion

    #region Update

    [HttpPut]
    [Permissions]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto request)
    {
        IActionResult? Validation = await HandleValidationAsync(new UpdateUserDtoValidator(), request);
        if (Validation is not null)
            return Validation;

        request.Id = HttpContext.GetUserId().Value;
        UpdateUserStatusDto status = await _mediator.Send(new UpdateUserCommand(request));
        if (status != UpdateUserStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion

    #region Delete

    [HttpDelete]
    [Permissions]
    public async Task<IActionResult> Delete()
    {
        DeleteUserStatusDto status = await _mediator.Send(new DeleteUserCommand(HttpContext.GetUserId().Value));
        if (status != DeleteUserStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion
}