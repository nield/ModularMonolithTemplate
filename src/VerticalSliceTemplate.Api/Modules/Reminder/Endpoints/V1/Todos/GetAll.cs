﻿using VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;

namespace VerticalSliceTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;

public sealed class GetAll : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/todos", Handler)
            .WithTags(TagContants.Todos)
            .WithDescription("Get all todos");
    }

    public sealed record Response(long Id, string Title, List<string> Tags);

    public static async Task<IEnumerable<Response>> Handler(
        IReminderDbContext applicationDbContext,
        CancellationToken cancellationToken)
    {
        var data = await applicationDbContext.TodoItems
            .AsNoTracking()
            .Select(x => new Response(x.Id, x.Title, x.Tags))
            .ToListAsync(cancellationToken);

        return data;
    }
}
