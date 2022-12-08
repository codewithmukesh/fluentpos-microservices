using BuildingBlocks.EFCore;
using FluentPOS.Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Data.Seeders
{
    public class ProductDataSeeder : IDataSeeder
    {
        private readonly CatalogDbContext _context;

        public ProductDataSeeder(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            if (!await _context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    Product.Create(1,"Samsung S24", "Top End", 1299),
                    Product.Create(2,"iPhone 15 Pro Max", "Top End", 2299),
                };
                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            }
        }
    }
}
