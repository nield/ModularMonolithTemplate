using ModularMonolithTemplate.Api.Modules.Reminder.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

[ExcludeFromCodeCoverage]
public class ReminderDbContextInitialiser
{
    private readonly ILogger<ReminderDbContextInitialiser> _logger;
    private readonly ReminderDbContext _context;
    private readonly IHostEnvironment _hostEnvironment;

    public ReminderDbContextInitialiser(
        ReminderDbContext context,
        IHostEnvironment hostEnvironment,
        ILogger<ReminderDbContextInitialiser> logger
    )
    {
        _logger = logger;
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task MigrateDatabaseAsync()
    {
        try
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Ensure existing seed data is saved before adding new data.
        await _context.Database.EnsureCreatedAsync();

        if (_hostEnvironment.IsTest())
        {
            _context.TodoItems.Add(new ToDoItem { Title = "Testing Item Init" });
        }

        await _context.SaveChangesAsync();
    }
}
