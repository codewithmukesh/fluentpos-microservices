namespace FluentPOS.Catalog.Products.Models;

public class Product
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }

    public static Product Create(string name, string description, decimal price)
    {
        var product = new Product() { Name = name, Description = description, Price = price };
        return product;
    }
}