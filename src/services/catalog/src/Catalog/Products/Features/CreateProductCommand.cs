using BuildingBlocks.CQRS;
using BuildingBlocks.Events;
using FluentPOS.Catalog.Data;
using FluentPOS.Catalog.Products.Models;
using FluentPOS.Shared.Events.Catalog;
using Mapster;

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
    private readonly IEventBus _eventBus;

    public CreateProductCommandHandler(CatalogDbContext context, IEventBus eventBus)
    {
        _context = context;
        _eventBus = eventBus;
    }

    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(command.Name!, command.Description!, command.Price);
        await _context.Products!.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync();

        var productCreatedEvent = product.Adapt<ProductCreatedEvent>();
        await _eventBus.PublishAsync(productCreatedEvent, token: cancellationToken);

        return product.Id;
    }
}