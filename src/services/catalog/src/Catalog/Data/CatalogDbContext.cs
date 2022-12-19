using BuildingBlocks.Auth;
using BuildingBlocks.EFCore;
using FluentPOS.Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Data;

public sealed class CatalogDbContext : DbContextBase
{
    public CatalogDbContext(DbContextOptions options, ICurrentUserService currentUser) : base(options, currentUser) { }
    public DbSet<Product>? Products { get; set; }
}