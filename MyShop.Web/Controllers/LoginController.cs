using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Common.Messages;
using MyShop.Application.Feature.User.Command;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Application.Feature.User.Queries;
using MyShop.Application.Feature.User.Validators;
using MyShop.Web.Filters.Permisions;

namespace MyShop.Web.Controllers;

public class LoginController(IMediator mediator, StatusMessageProvider responseMessage) :ApiBaseController(mediator, responseMessage)
{

    #region GetLogin

    [HttpPost]
    public async Task<IActionResult> GetLogin([FromBody] LoginUserDto loginUser)
    {
        
        IActionResult? validation = await HandleValidationAsync(new LoginUserDtoValidator(), loginUser);
        if (validation is not null)
            return validation;

        LoginUserStatusDtoClass login = await Mediator.Send(new LoginUserQueries(loginUser));

        if (login.LoginUserStatusDto != LoginUserStatusDto.Success)
            return BadRequestResponse(login.LoginUserStatusDto);

        return OkResponse(login);

    }

    #endregion

    #region Register

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        IActionResult? validation = await HandleValidationAsync(new RegisterUserDtoValidator(), registerUserDto);
        if (validation is not null)
            return validation;

        RegisterUserStatusDto status = await Mediator.Send(new RegisterUserCommand(registerUserDto));
        if (status != RegisterUserStatusDto.Success)
            return BadRequestResponse(status);
        
        return OkResponseNoData(status);
    }

    #endregion
    
}