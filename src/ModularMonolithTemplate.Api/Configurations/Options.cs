using ModularMonolithTemplate.Api.Common.Settings;

namespace ModularMonolithTemplate.Api.Configurations;

internal static class Options
{
    internal static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppSettings>(config);

        services.Configure<MassTransitSettings>(config.GetSection("MassTransit"));
    }
}
