namespace VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;

public interface IToDoRepository : IRepository<ToDoItem>
{
    Task DeleteAll(CancellationToken cancellationToken = default);
}
