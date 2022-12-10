using BuildingBlocks.Events;

namespace FluentPOS.Shared.Events.Catalog;

public class ProductCreatedEvent : BaseEvent
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
