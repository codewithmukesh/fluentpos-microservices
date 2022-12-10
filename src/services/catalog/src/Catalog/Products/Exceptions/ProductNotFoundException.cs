using System.Net;
using BuildingBlocks.Exceptions;

namespace FluentPOS.Catalog.Products.Exceptions;

internal class ProductNotFoundException : CustomException
{
    public ProductNotFoundException(int productId) : base(string.Format("Product with ID {0} Not Found.", productId), null, HttpStatusCode.NotFound)
    {
    }

    public ProductNotFoundException(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, errors, statusCode)
    {
    }
}
