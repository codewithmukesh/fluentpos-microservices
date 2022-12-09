using BuildingBlocks.CQRS;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Products.Models;

namespace FluentPOS.Catalog.Products.Features;

public class CreateProductCommand : ICommand<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly CatalogDbContext _context;

    public CreateProductCommandHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.Create(command.Name!, command.Description!, command.Price);
        await _context.Products!.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync();

        return product.Id;
    }
}