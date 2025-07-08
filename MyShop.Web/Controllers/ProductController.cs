using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Common.Messages;
using MyShop.Application.Extensions;
using MyShop.Application.Feature.Product.Command;
using MyShop.Application.Feature.Product.DTOs;
using MyShop.Application.Feature.Product.Queries;
using MyShop.Application.Feature.Product.Validators;
using MyShop.Web.Filters.Permisions;

namespace MyShop.Web.Controllers;

public class ProductController(IMediator mediator, StatusMessageProvider responseMessage) :ApiBaseController(mediator, responseMessage)
{
    #region GetAll

    [HttpGet]
    public async Task<IActionResult> GetAll([FromBody] SearchProductDto request)
    {
        SearchProductDto Model = await Mediator.Send(new ListProductQueries(request));
        if (Model.Entities.IsNullOrEmpty())
            return BadRequestResponse(null, 414);

        return OkResponse(Model);
    }

    #endregion

    #region GetById

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        ProductDto model = await Mediator.Send(new GetProductQueries(id));
        if (model.ModelIsNull())
            return BadRequestResponse(null, 414);

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
        
        CreateProductStatusDto status = await Mediator.Send(new CreateProductCommand(request));
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
        
        UpdateProductStatusDto status = await Mediator.Send(new UpdateProductCommand(request));
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
            UserId = 0,
            ProductId = id
        };
        DeleteProductStatusDto status = await Mediator.Send(new DeleteProductCommand(Dto));
        if (status != DeleteProductStatusDto.Success)
            return BadRequestResponse(status);

        return OkResponseNoData(status);
    }

    #endregion
}