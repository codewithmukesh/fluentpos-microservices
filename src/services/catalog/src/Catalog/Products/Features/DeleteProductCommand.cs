using BuildingBlocks.CQRS;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Products.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Catalog.Products.Features;

public class DeleteProductCommand : ICommand<int>
{
    public DeleteProductCommand(int productId)
    {
        Id = productId;
    }
    public int Id { get; set; }
}
public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, int>
{
    private readonly CatalogDbContext _context;

    public DeleteProductCommandHandler(CatalogDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _context.Products!.AsQueryable().SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        if (product is null) throw new ProductNotFoundException(command.Id);
        _context.Products!.Remove(product);
        await _context.SaveChangesAsync();

        return product.Id;
    }
}
