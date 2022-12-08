using BuildingBlocks.Cache;
using BuildingBlocks.CQRS;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Products.Dtos;
using FluentPOS.Catalog.Products.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Products.Features;

public class GetProductByIdQuery : IQuery<ProductResponseDto>, ICacheRequest
{
    public int Id { get; set; }
    public string? CacheKey => nameof(GetProductByIdQuery);

    public GetProductByIdQuery(int id) => Id = id;
}

internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponseDto>
{
    private readonly CatalogDbContext _catalogDbContext;
    public GetProductByIdQueryHandler(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }
    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _catalogDbContext.Products.AsQueryable().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null) throw new ProductNotFoundException(request.Id);
        //TODO : Add Automapper here
        return new ProductResponseDto { Id = 1, Description = "test", Name = "test", Price = 10 };
    }
}