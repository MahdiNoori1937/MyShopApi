using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Commonn.Messages;
using MyShop.Application.Feature.User.Command;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Application.Feature.User.Queries;
using MyShop.Application.Feature.User.Validators;
using MyShop.Web.Filters.Permisions;

namespace MyShop.Web.Controllers;

public class LoginController(IMediator mediator, StatusMessageProvider responseMessage) :ApiBaseController(mediator, responseMessage)
{

    [HttpPost]
    public async Task<IActionResult> GetLogin([FromBody] LoginUserDto loginUser)
    {
        
        IActionResult? Validation = await HandleValidationAsync(new LoginUserDtoValidator(), loginUser);
        if (Validation is not null)
            return Validation;

        LoginUserStatusDtoClass login = await _mediator.Send(new LoginUserQueries(loginUser));

        if (login.LoginUserStatusDto != LoginUserStatusDto.Success)
            return BadRequestResponse(login.LoginUserStatusDto);

        return OkResponse(login);

    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        IActionResult? Validation = await HandleValidationAsync(new RegisterUserDtoValidator(), registerUserDto);
        if (Validation is not null)
            return Validation;

        RegisterUserStatusDto status = await _mediator.Send(new RegisterUserCommand(registerUserDto));
        if (status != RegisterUserStatusDto.Success)
            return BadRequestResponse(status);
        
        return OkResponseNoData(status);
    }
    
}