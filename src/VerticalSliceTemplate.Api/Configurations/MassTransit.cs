using MassTransit;
using System.Reflection;
using VerticalSliceTemplate.Api.Common.Infrastructure.Messaging;

namespace VerticalSliceTemplate.Api.Configurations;

public static class MassTransit
{
    public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        MessageCorrelation.UseCorrelationId<BaseMessage>((x) =>
        {
            if (Guid.TryParse(x.CorrelationId, out Guid correlationId))
            {
                return correlationId;
            }

            return Guid.NewGuid();
        });

        if (!IsMassTransitEnabled(configuration))
        {
            services.AddScoped<IPublishMessageService, MockPublishMessageService>();
            return;
        }

        services.AddScoped<IPublishMessageService, PublishMessageService>();

        services.AddMassTransit(config =>
        {
            config.AddTelemetryListener(true);

            config.AddConsumers(Assembly.GetEntryAssembly());

            config.ConfigureHealthCheckOptions(options => options.Name = "MassTransit");

            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var rabbitUri = configuration.GetConnectionString("RabbitMq");

                ArgumentException.ThrowIfNullOrWhiteSpace(rabbitUri);

                rabbitConfig.Host(new Uri(rabbitUri));

                rabbitConfig.ConfigureEndpoints(context);
            });
        });
    }

    private static bool IsMassTransitEnabled(IConfiguration configuration) => 
        configuration.GetValue<bool>("MassTransit:PublishEnabled");
}
