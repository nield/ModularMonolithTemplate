using ModularMonolithTemplate.Api.Modules.Reminder;

namespace ModularMonolithTemplate.Api.Configurations;

internal static class Migrations
{
    internal static async Task ApplyMigrations(this WebApplication webApplication)
    {
        await webApplication.Services.MigrateReminderDatabase();
    }
}
