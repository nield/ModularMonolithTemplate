using ModularMonolithTemplate.Api.Modules.Reminder.Public;
using Refit;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ModularMonolithTemplate.Api.Configurations;

internal static class HttpClients
{
    private static readonly RefitSettings _defaultRefitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
    };

    internal static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var reminderUri = configuration.GetValue<string>("ExternalServices:Reminder:BaseUrl");

        ArgumentNullException.ThrowIfNullOrWhiteSpace(reminderUri);

        services.AddRefitClient<IReminderService>(_defaultRefitSettings)
            .ConfigureHttpClient((sp, config) =>
            {
                using var scope = sp.CreateScope();

                var currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();

                var token = currentUserService.Token?.Replace("Bearer", "")?.Trim();

                config.BaseAddress = new Uri(reminderUri);
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            });
    }
}
