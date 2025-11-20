using ModularMonolithTemplate.Api.Modules.Reminder.Public;
using ModularMonolithTemplate.Api.Modules.Weather.Common.Contants;

namespace ModularMonolithTemplate.Api.Modules.Weather.Endpoints.V1;

public sealed class GetForecast : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGetRoute("/weather/forecast", Handler)
            .WithDescription("Get weather forecast")
            .WithTags(TagConstants.Weather);
    }

    public static async Task<WeatherForecast[]> Handler(
        IReminderService reminderService,
        CancellationToken cancellationToken = default)
    {
        var data = await reminderService.GetAllToDosAsync(cancellationToken);

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }

    public sealed record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);
}
