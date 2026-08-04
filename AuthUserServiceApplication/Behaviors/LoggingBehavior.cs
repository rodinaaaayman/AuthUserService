using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUserServiceApplication.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Starting {RequestName}", requestName);

        var response = await next();

        _logger.LogInformation("Finished {RequestName}", requestName);

        return response;
    }
}