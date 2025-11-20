using ModularMonolithTemplate.Api.Modules.Reminder.Common.Constants;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;
using ModularMonolithTemplate.Api.Modules.Reminder.Public.Messages;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;

public sealed class Create : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPostRoute("/todos", Handler)
            .WithTags(TagContants.Todos)
            .WithDescription("Create new todo");
    }

    public static async Task<CreatedAtRoute<Response>> Handler(
        [Validate] Request request,
        IToDoRepository toDoRepository,
        IPublishMessageService publishMessageService,
        CancellationToken cancellationToken)
    {
        var newTodoItem = new ToDoItem
        {
            Title = request.Title,
            Tags = request.Tags
        };

        await toDoRepository.AddAsync(newTodoItem, cancellationToken);

        var createdToDo = new ToDoCreated
        {
            Id = newTodoItem.Id,
            Title = newTodoItem.Title,
        };

        await publishMessageService.Publish(createdToDo, cancellationToken);

        return TypedResults.CreatedAtRoute(
            new Response { Id = newTodoItem.Id }, "GetToDoById", new { id = newTodoItem.Id });
    }

    public sealed class Request
    {
        public required string Title { get; set; }
        public List<string> Tags { get; set; } = [];
    }

    public sealed class Response
    {
        public required long Id { get; set; }
    }

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
