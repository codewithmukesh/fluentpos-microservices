using BuildingBlocks.Auth;
using BuildingBlocks.EFCore;
using FluentPOS.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Application.Data;

public sealed class CatalogDbContext : DbContextBase
{
    public CatalogDbContext(DbContextOptions options, IAuthenticatedUser currentUser) : base(options, currentUser) { }
    public DbSet<Product>? Products { get; set; }
}