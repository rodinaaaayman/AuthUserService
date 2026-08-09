using AuthUserServiceApplication.IntegrationMessages;
using AuthUserServiceApplication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
public class ClientStatusRequestConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;
    private const string RequestQueueName = "client_status_requests";

    public ClientStatusRequestConsumer(IServiceScopeFactory scopeFactory, IConfiguration config)
    {
        _scopeFactory = scopeFactory;
        _config = config;
    }

    public override async Task StartAsync(CancellationToken ct)
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["RabbitMq:HostName"],
            Port = int.Parse(_config["RabbitMq:Port"]!),
            UserName = _config["RabbitMq:UserName"],
            Password = _config["RabbitMq:Password"]
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(RequestQueueName, durable: true, exclusive: false, autoDelete: false);

        await base.StartAsync(ct);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel!);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var request = JsonSerializer.Deserialize<ClientStatusRequest>(json);

            ClientStatusResponse response;

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var client = await context.Clients.FindAsync(request!.Id);

                response = new ClientStatusResponse
                {
                    Id = request.Id,
                    Exists = client is not null,
                    IsActive = client?.IsActive ?? false
                };
            }

            var replyJson = JsonSerializer.Serialize(response);
            var replyBody = Encoding.UTF8.GetBytes(replyJson);

            var replyProps = new BasicProperties
            {
                CorrelationId = ea.BasicProperties.CorrelationId
            };

            await _channel!.BasicPublishAsync(
                exchange: "",
                routingKey: ea.BasicProperties.ReplyTo!,
                mandatory: false,
                basicProperties: replyProps,
                body: replyBody);

            await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
        };

        await _channel!.BasicConsumeAsync(RequestQueueName, autoAck: false, consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken ct)
    {
        if (_channel is not null) await _channel.CloseAsync();
        if (_connection is not null) await _connection.CloseAsync();
        await base.StopAsync(ct);
    }
}