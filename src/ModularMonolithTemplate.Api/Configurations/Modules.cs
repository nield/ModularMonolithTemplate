using ModularMonolithTemplate.Api.Modules.Reminder;

namespace ModularMonolithTemplate.Api.Configurations;

public static class Modules
{
    internal static IHostApplicationBuilder ConfigureModules(this IHostApplicationBuilder builder)
    {
        builder.SetupReminderModule();

        return builder;
    }
}