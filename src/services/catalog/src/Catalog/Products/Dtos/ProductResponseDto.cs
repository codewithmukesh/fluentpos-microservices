namespace FluentPOS.Catalog.Products.Dtos;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}