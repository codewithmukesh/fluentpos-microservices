namespace BuildingBlocks.Events;
public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, string[]? topics = default, CancellationToken token = default)
            where TEvent : IDomainEvent;

    Task SubscribeAsync<TEvent>(string[]? topics = default, CancellationToken token = default)
        where TEvent : IDomainEvent;
}
