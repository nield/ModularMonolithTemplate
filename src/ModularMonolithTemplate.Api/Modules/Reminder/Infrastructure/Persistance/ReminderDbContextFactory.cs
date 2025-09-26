using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Design;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

[ExcludeFromCodeCoverage]
public class ReminderDbContextFactory : IDesignTimeDbContextFactory<ReminderDbContext>
{
    public ReminderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ReminderDbContext>();

        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

        optionsBuilder.UseSqlServer(
            config.GetConnectionString("SqlDatabase"));

        return new ReminderDbContext(optionsBuilder.Options);
    }
}