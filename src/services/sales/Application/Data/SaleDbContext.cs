using BuildingBlocks.Auth;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Sales.Domain;

namespace Sales.Data;
public sealed class SaleDbContext : DbContextBase
{
    public SaleDbContext(DbContextOptions options, IAuthenticatedUser currentUser) : base(options, currentUser) { }
    public DbSet<Sale>? Sales { get; set; }
    public DbSet<SaleItem>? SaleItems { get; set; }
}