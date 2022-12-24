using BuildingBlocks.Dto;

namespace FluentPOS.Catalog.Products.Dtos;

public class ProductResponseDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}