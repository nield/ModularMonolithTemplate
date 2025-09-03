using Microsoft.EntityFrameworkCore.Diagnostics;
using VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;

namespace VerticalSliceTemplate.Api.Modules.Reminder.Infrastructure;

internal static class Configuration
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
}
