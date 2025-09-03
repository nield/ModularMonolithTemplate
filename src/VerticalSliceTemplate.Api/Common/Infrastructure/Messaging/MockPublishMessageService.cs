namespace VerticalSliceTemplate.Api.Common.Infrastructure.Messaging;

public class MockPublishMessageService : IPublishMessageService
{
    private readonly ILogger<MockPublishMessageService> _logger;

    public MockPublishMessageService(ILogger<MockPublishMessageService> logger)
    {
        _logger = logger;
    }

    public Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : BaseMessage
    {
        _logger.LogInformation("Message published. CorrelationId: {CorrelationId}", message.CorrelationId);

        return Task.CompletedTask;
    }
}
