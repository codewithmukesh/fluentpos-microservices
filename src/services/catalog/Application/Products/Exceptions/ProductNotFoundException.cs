using BuildingBlocks.Exceptions;
using System.Net;

namespace FluentPOS.Catalog.Application.Products.Exceptions;

internal class ProductNotFoundException : CustomException
{
    public ProductNotFoundException(Guid productId) : base(string.Format("Product with ID {0} Not Found.", productId), null, HttpStatusCode.NotFound)
    {
    }
}
