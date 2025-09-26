using Refit;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Public;

/// <summary>
/// Used for cross module communication
/// </summary>
public interface IReminderService
{
    [Get("/api/v1/todos")]
    Task<IEnumerable<Endpoints.V1.Todos.GetAll.Response>> GetAllToDosAsync();
}
