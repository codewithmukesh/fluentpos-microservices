using MassTransit;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Events;
internal class MassTransitEventBus : IEventBus
{
    private readonly IPublishEndpoint _endpoint;
    private readonly ILogger<MassTransitEventBus> _logger;

    public MassTransitEventBus(IPublishEndpoint publishEndpoint, ILogger<MassTransitEventBus> logger)
    {
        _endpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, string[]? topics = null, CancellationToken token = default) where TEvent : IDomainEvent
    {
        await _endpoint.Publish(@event, token);
    }

    public Task SubscribeAsync<TEvent>(string[]? topics = null, CancellationToken token = default) where TEvent : IDomainEvent
    {
        throw new NotImplementedException();
    }
}
