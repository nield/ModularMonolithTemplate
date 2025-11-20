using ModularMonolithTemplate.Api.Modules.Reminder.Entities;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;

public interface IReminderQueryDbContext
{
    DbSet<ToDoItem> TodoItems { get; }
}