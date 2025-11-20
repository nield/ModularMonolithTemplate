using ModularMonolithTemplate.Api.Modules.Reminder.Common.Constants;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;

public sealed class GetById : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos/{id}", Handler)
            .WithTags(TagContants.Todos)
            .WithDescription("Get a single ToDo")
            .WithName("GetToDoById");
    }

    public static async Task<Response> Handler(
        [Required] long id,
        IToDoRepository toDoRepository,
        CancellationToken cancellationToken)
    {
        var todo = await toDoRepository.GetByIdAsync(id, cancellationToken);

        if (todo is null)
        {
            throw new NotFoundException(nameof(ToDoItem), id);
        }

        return new Response(todo.Id, todo.Title, todo.Tags);
    }

    public sealed record Response(long Id, string Title, List<string> Tags);
}
