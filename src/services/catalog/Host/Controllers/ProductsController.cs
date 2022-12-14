using System.Net;
using BuildingBlocks.Web;
using FluentPOS.Catalog.Application.Products.Features;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluentPOS.Catalog.Host.Controllers;

public class ProductsController : BaseController
{
    [HttpGet("{productId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerOperation(Summary = "gets product by id.", Description = "gets product by id.")]
    public async Task<IActionResult> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(productId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [SwaggerOperation(Summary = "creates a new product and returns id.", Description = "creates a new product and returns id.")]
    public async Task<IActionResult> CreateAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Created(nameof(CreateAsync), new { id = result });
    }

    [HttpDelete("{productId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerOperation(Summary = "deletes a product by id.", Description = "deletes a product by id.")]
    public async Task<IActionResult> DeleteAsync(Guid productId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteProductCommand(productId), cancellationToken);
        return NoContent();
    }

    [HttpPost("search")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [SwaggerOperation(Summary = "search for products.", Description = "search for products.")]
    public async Task<IActionResult> SearchAsync(SearchProductsCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}