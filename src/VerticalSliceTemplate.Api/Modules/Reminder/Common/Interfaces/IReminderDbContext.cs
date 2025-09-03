namespace VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;

public interface IReminderDbContext
{
    DbSet<ToDoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}