using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;
using System.Reflection;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

public class ReminderDbContext : DbContext, IReminderQueryDbContext
{
    private const string _configNamespace = "ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance.Configuration";

    public static readonly string MigrationTableName = "__EFMigrationsHistory";
    public static readonly string DbSchema = "reminders";

    #region DbSets

    public DbSet<ToDoItem> TodoItems => Set<ToDoItem>();

    #endregion

    public ReminderDbContext(
        DbContextOptions<ReminderDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == _configNamespace);
        modelBuilder.HasDefaultSchema(DbSchema);

        base.OnModelCreating(modelBuilder);
    }
}
