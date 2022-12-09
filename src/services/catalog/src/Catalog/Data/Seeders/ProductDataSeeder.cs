using BuildingBlocks.EFCore;
using FluentPOS.Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Catalog.Data.Seeders;

public class ProductDataSeeder : IDataSeeder
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<ProductDataSeeder> _logger;

    public ProductDataSeeder(CatalogDbContext context, ILogger<ProductDataSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAllAsync()
    {
        if (!await _context.Products!.AnyAsync())
        {
            _logger.LogInformation("Data Seeding for Products Started.");
            var products = new List<Product>
            {
                Product.Create("Samsung S24", "Top End", 1299),
                Product.Create("iPhone 15 Pro Max", "Top End", 2299),
            };
            await _context.Products!.AddRangeAsync(products);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Data Seeding for Products Completed.");
        }
    }
}
