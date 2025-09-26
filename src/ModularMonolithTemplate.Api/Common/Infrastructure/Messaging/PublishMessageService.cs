using MassTransit;

namespace ModularMonolithTemplate.Api.Common.Infrastructure.Messaging;

public class PublishMessageService : IPublishMessageService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<PublishMessageService> _logger;

    public PublishMessageService(
        IPublishEndpoint publishEndpoint,
        ILogger<PublishMessageService> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : BaseMessage
    {
        try
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed publishing message. Message: {Message}. CorrelationId: {CorrelationId}", message, message.CorrelationId);
            throw;
        }
    }
}