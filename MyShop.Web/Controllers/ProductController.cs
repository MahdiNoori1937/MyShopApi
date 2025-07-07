using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Commonn.Messages;
using MyShop.Application.Extensions;
using MyShop.Application.Feature.Product.Command;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.Product.Queries;
using MyShop.Application.Feature.Product.Validators;
using MyShop.Web.Extensions;
using MyShop.Web.Filters.Permisions;

namespace MyShop.Web.Controllers;

public class ProductController(IMediator mediator, StatusMessageProvider responseMessage) :ApiBaseController(mediator, responseMessage)
{
    #region GetAll

    [HttpGet]
    public async Task<IActionResult> GetAll([FromBody] SearchProductDto request)
    {
        SearchProductDto Model = await _mediator.Send(new ListProductQueries(request));
        if (Model.Entities.IsNullOrEmpty())
            return BadRequestResponse(null, 404);

        return OkResponse(Model);
    }

    #endregion

    #region GetById

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        ProductDto model = await _mediator.Send(new GetProductQueries(id));
        if (model.ModelIsNull())
            return BadRequestResponse(null, 400);

        return OkResponse(model);
    }

    #endregion

    #region Create

    [HttpPost]
    [Permissions]
    public async Task<IActionResult> Create([FromBody] CreateProductDto request)
    {
        IActionResult? Validation = await HandleValidationAsync(new CreateProductDtoValidator(), request);
        if (Validation is not null)
            return Validation;

        CreateProductStatusDto status = await _mediator.Send(new CreateProductCommand(request));
        if (status != CreateProductStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion

    #region Update

    [HttpPut]
    [Permissions]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto request)
    {
        IActionResult? Validation = await HandleValidationAsync(new UpdateProductDtoValidator(), request);
        if (Validation is not null)
            return Validation;

        request.Id = HttpContext.GetUserId().Value;
        UpdateProductStatusDto status = await _mediator.Send(new UpdateProductCommand(request));
        if (status != UpdateProductStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion

    #region Delete

    [HttpDelete]
    [Permissions]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        DeleteProductDto Dto = new()
        {
            UserId = HttpContext.GetUserId().Value,
            ProductId = id
        };
        DeleteProductStatusDto status = await _mediator.Send(new DeleteProductCommand(Dto));
        if (status != DeleteProductStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion
}