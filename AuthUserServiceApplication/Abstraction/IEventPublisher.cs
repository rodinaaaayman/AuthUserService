public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, string routingKey);
}