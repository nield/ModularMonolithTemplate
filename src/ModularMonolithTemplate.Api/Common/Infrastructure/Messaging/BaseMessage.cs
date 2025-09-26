namespace ModularMonolithTemplate.Api.Common.Infrastructure.Messaging;

public abstract class BaseMessage
{
    public string? CorrelationId { get; set; }
    public DateTimeOffset CreatedDateTime { get; } = DateTimeOffset.UtcNow;
}