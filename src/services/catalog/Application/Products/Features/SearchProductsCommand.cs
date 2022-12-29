using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using BuildingBlocks.Specification;
using FluentPOS.Catalog.Application.Data;
using FluentPOS.Catalog.Application.Products.Dtos;
using FluentPOS.Catalog.Application.Products.Specifications;
using FluentPOS.Catalog.Domain;
using MediatR;

namespace FluentPOS.Catalog.Application.Products.Features;
public class SearchProductsCommand : ICommand<PaginatedDtoResponse<ProductResponseDto>>, IPaginationCommand
{
    public SearchProductsCommand(string searchString, int pageNumber, int pageSize, decimal minimumPrice = decimal.Zero, decimal maximumPrice = decimal.MaxValue)
    {
        SearchString = searchString;
        MinimumPrice = minimumPrice;
        MaximumPrice = maximumPrice;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public string? SearchString { get; set; }
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class SearchProductsCommandHandler : IRequestHandler<SearchProductsCommand, PaginatedDtoResponse<ProductResponseDto>>
{
    private readonly CatalogDbContext _catalogDbContext;

    public SearchProductsCommandHandler(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task<PaginatedDtoResponse<ProductResponseDto>> Handle(SearchProductsCommand command, CancellationToken cancellationToken)
    {
        var searchProductsSpecification = new SearchProductsSpecification(command.SearchString!, command.MinimumPrice, command.MaximumPrice);
        var data = await _catalogDbContext.Products!.Specify(searchProductsSpecification).ToPaginatedDtoListAsync<Product, ProductResponseDto>(command.PageSize, command.PageNumber);
        return data;
    }
}