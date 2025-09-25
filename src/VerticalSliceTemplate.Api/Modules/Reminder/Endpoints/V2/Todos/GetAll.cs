namespace VerticalSliceTemplate.Api.Modules.Reminder.Endpoints.V2.Todos;

public sealed class GetAll : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos", Handler, 2)
            .WithTags(TagContants.Todos)
            .WithDescription("Used to get all todos");
    }

    public static async Task<IEnumerable<Response>> Handler(
        IReminderDbContext applicationDbContext,
        CancellationToken cancellationToken)
    {
        var data = await applicationDbContext.TodoItems
            .Select(x => new Response(x.Id, x.Title, x.Tags, x.CreatedBy))
            .ToListAsync(cancellationToken);

        return data;
    }

    public sealed record Response(long Id, string Title, List<string> Tags, string? CreatedBy);
}
