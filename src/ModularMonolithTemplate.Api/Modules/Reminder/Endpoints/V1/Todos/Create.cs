using ModularMonolithTemplate.Api.Modules.Reminder.Common.Constants;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;
using ModularMonolithTemplate.Api.Modules.Reminder.Public.Messages;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;

public sealed class Create : IEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPostRoute("/todos", 
            async ([Validate] Request request, Handler handler, CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                return TypedResults.CreatedAtRoute(
                    new Response { Id = response.Id }, "GetToDoById", new { id = response.Id });
            })
            .WithTags(TagContants.Todos)
            .WithDescription("Create new todo");
    }

    /// <summary>
    /// This handler example should be used for more complex endpoints with in-depth business logic.
    /// This example is simple, but this shows how the framework is intended to be used when complex logic is involved.
    /// Benefit of this approach is individual methods in class can be unit tested. 
    /// </summary>
    public sealed class Handler(
        IToDoRepository toDoRepository,
        IPublishMessageService publishMessageService) : IEndpointHandler
    {
        public async Task<Response> Handle(
            Request request, CancellationToken cancellationToken)
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
            
            return new Response { Id = newTodoItem.Id };
        }
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
