using BuildingBlocks.Cache;
using BuildingBlocks.CQRS;
using FluentPOS.Catalog.Application.Data;
using FluentPOS.Catalog.Application.Products.Dtos;
using FluentPOS.Catalog.Application.Products.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Application.Products.Features;

public class GetProductByIdQuery : IQuery<ProductResponseDto>, ICacheRequest
{
    public Guid Id { get; set; }
    public string? CacheKey => nameof(GetProductByIdQuery);

    public GetProductByIdQuery(Guid id) => Id = id;
}

internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponseDto>
{
    private readonly CatalogDbContext _catalogDbContext;

    public GetProductByIdQueryHandler(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task<ProductResponseDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _catalogDbContext.Products!.AsQueryable().SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        if (product is null) throw new ProductNotFoundException(query.Id);
        var productDto = product.Adapt<ProductResponseDto>();
        return productDto;
    }
}