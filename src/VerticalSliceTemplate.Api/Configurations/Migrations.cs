using VerticalSliceTemplate.Api.Modules.Reminder;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class Migrations
{
    internal static async Task ApplyMigrations(this WebApplication webApplication)
    {
        List<Task> tasks = []; 
      
        tasks.Add(webApplication.MigrateReminderDatabase());

        await Task.WhenAll(tasks);
    }
}
