namespace BuildingBlocks.Events;

public abstract class BaseEvent : IDomainEvent
{
    public string EventType { get { return GetType().FullName!; } }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
}
