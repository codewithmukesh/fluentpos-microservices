using BuildingBlocks.Events;

namespace FluentPOS.Shared.Events.Catalog;

public class ProductCreatedEvent : BaseEvent
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
}
