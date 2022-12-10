using BuildingBlocks.Domain;

namespace FluentPOS.Catalog.Products.Models;

public class Product : AuditableEntity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }

    public Product(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}