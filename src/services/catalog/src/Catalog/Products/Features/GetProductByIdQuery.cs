using BuildingBlocks.Cache;
using BuildingBlocks.CQRS;
using FluentPOS.Catalog.Products.Dtos;

namespace FluentPOS.Catalog.Products.Features;

public class GetProductByIdQuery : IQuery<ProductResponseDto>, ICacheRequest
{
    public int Id { get; set; }
    public string? CacheKey => nameof(GetProductByIdQuery);

    public GetProductByIdQuery(int id) => Id = id;
}

internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponseDto>
{
    public Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ProductResponseDto { Id = 1, Description = "test", Name = "test", Price = 10 });
    }
}