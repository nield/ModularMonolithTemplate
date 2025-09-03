namespace VerticalSliceTemplate.Api.Configurations;

internal static class Migrations
{
    internal static async Task ApplyMigrations(this WebApplication webApplication)
    {
        List<Task> tasks = []; 
      
        tasks.Add(MigrateReminderDatabase(webApplication));

        await Task.WhenAll(tasks);
    }

    private static async Task MigrateReminderDatabase(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ReminderDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();

        await dbContextInitialiser.SeedDataAsync();
    }
}
