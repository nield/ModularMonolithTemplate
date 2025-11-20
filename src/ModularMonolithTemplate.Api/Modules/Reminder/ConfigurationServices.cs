using Microsoft.EntityFrameworkCore.Diagnostics;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;
using ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

namespace ModularMonolithTemplate.Api.Modules.Reminder;

public static class ConfigurationServices
{
    internal static void SetupReminderModule(this IHostApplicationBuilder builder)
    {
        builder.SetupReminderDatabase();
    }

    private static void SetupReminderDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ReminderDbContextInitialiser>();

        builder.Services.AddScoped<IReminderQueryDbContext>(provider =>
            provider.GetRequiredService<ReminderDbContext>());

        builder.Services.AddDbContext<ReminderDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"), config =>
            {
                config.MigrationsHistoryTable(ReminderDbContext.MigrationTableName, ReminderDbContext.DbSchema);
            })
            .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
        }, ServiceLifetime.Scoped);

        builder.EnrichSqlServerDbContext<ReminderDbContext>();
    }

    public static async Task MigrateReminderDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ReminderDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();

        await dbContextInitialiser.SeedDataAsync();
    }
}
