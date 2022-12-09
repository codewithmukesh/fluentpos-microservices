using FluentPOS.Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Data;

public sealed class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
    public DbSet<Product>? Products { get; set; }
}