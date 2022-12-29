using BuildingBlocks.Domain;

namespace FluentPOS.Catalog.Domain;

public class Product : AuditableEntity
{
    public string? Name { get; private set; }
    public string? SecondaryName { get; private set; }
    public string? Details { get; private set; }
    public decimal Price { get; private set; }
    public string? Slug { get; private set; }
    public bool TrackQuantity { get; private set; }
    public decimal Quantity { get; private set; }

    public Product(string name, string details, decimal price)
    {
        Name = name;
        Details = details;
        Price = price;
    }
}