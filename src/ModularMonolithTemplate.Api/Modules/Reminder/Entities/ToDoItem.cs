namespace ModularMonolithTemplate.Api.Modules.Reminder.Entities;

public class ToDoItem : BaseAuditableEntity
{
    public required string Title { get; set; }
    public List<string> Tags { get; set; } = [];
}
