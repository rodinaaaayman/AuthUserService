using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqEventPublisher : IEventPublisher, IAsyncDisposable
{
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly SemaphoreSlim _initLock = new(1, 1);
    private const string ExchangeName = "user_events";

    public RabbitMqEventPublisher(IConfiguration config)
    {
        _config = config;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_channel is not null) return;

        await _initLock.WaitAsync();
        try
        {
            if (_channel is not null) return;

            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMq:HostName"],
                Port = int.Parse(_config["RabbitMq:Port"]!),
                UserName = _config["RabbitMq:UserName"],
                Password = _config["RabbitMq:Password"]
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Fanout, durable: true);
        }
        finally
        {
            _initLock.Release();
        }
    }

    public async Task PublishAsync<T>(T @event, string routingKey)
    {
        await EnsureInitializedAsync();

        var json = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(json);

        var props = new BasicProperties { Persistent = true };

        await _channel!.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: props,
            body: body);
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null) await _channel.DisposeAsync();
        if (_connection is not null) await _connection.DisposeAsync();
    }
}