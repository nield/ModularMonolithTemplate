using Microsoft.EntityFrameworkCore.Diagnostics;

namespace VerticalSliceTemplate.Api.Modules.Reminder;

internal static class ConfigurationServices
{
    internal static void SetupReminderDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ReminderDbContextInitialiser>();

        builder.Services.AddScoped<IReminderDbContext>(provider =>
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

    internal static async Task MigrateReminderDatabase(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ReminderDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();

        await dbContextInitialiser.SeedDataAsync();
    }
}
