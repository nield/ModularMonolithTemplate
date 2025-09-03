using System.Reflection;

namespace VerticalSliceTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

public class ReminderDbContext : DbContext, IReminderDbContext
{
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
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(DbSchema);

        base.OnModelCreating(modelBuilder);
    }
}
