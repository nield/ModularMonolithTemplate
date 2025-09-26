using Refit;
using System.Text.Json;
using ModularMonolithTemplate.Api.Modules.Reminder.Public;

namespace ModularMonolithTemplate.Api.Configurations;

internal static class HttpClients
{
    internal static readonly RefitSettings DefaultRefitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
    };

    internal static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var reminderUri = configuration.GetValue<string>("ExternalServices:Reminder:BaseUrl");

        ArgumentNullException.ThrowIfNullOrWhiteSpace(reminderUri);

        services.AddRefitClient<IReminderService>(DefaultRefitSettings)
            .ConfigureHttpClient(config => config.BaseAddress = new Uri(reminderUri));
    }
}
