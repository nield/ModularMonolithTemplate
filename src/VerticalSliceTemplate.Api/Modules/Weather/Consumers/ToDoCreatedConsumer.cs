using MassTransit;

namespace VerticalSliceTemplate.Api.Modules.Weather.Consumers;

/// <summary>
/// Sample consumer for cross module event
/// </summary>
public class ToDoCreatedConsumer : IConsumer<ToDoCreated>
{
    private readonly ILogger<ToDoCreatedConsumer> _logger;

    public ToDoCreatedConsumer(ILogger<ToDoCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ToDoCreated> context)
    {
        _logger.LogInformation("Received message. MessageId: {MessageId}", context.MessageId);

        return Task.CompletedTask;
    }
}
