using VerticalSliceTemplate.Api.Common.Infrastructure.Messaging;

namespace VerticalSliceTemplate.Api.Modules.Reminder.Public.Messages;

public class ToDoCreated : BaseMessage
{
    public long Id { get; set; }
    public required string Title { get; set; }
}
