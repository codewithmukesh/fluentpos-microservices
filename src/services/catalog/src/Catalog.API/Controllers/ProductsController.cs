using BuildingBlocks.Web;
using FluentPOS.Catalog.Products.Dtos;
using FluentPOS.Catalog.Products.Features;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluentPOS.Catalog.API.Controllers;

public class ProductsController : BaseController
{
    [HttpGet("{productId:int}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "gets product by id.", Description = "gets product by id.")]
    public async Task<IActionResult> GetByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(productId), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [SwaggerOperation(Summary = "creates a new product and returns id.", Description = "creates a new product and returns id.")]
    public async Task<IActionResult> CreateAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "deletes a product by id.", Description = "deletes a product by id.")]
    public async Task<IActionResult> DeleteAsync(int productId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteProductCommand(productId), cancellationToken);

        return Ok(result);
    }
}