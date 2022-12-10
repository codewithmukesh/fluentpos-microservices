using MediatR;

namespace BuildingBlocks.Events;
public interface IDomainEvent : INotification
{
    DateTime CreatedAt { get; }
    IDictionary<string, object> MetaData { get; }
}