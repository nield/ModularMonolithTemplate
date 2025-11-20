using ModularMonolithTemplate.Api.Common.Infrastructure.Persistance;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance.Repositories;

public class ToDoRepository : BaseRepository<ToDoItem>, IToDoRepository
{
    public ToDoRepository(ReminderDbContext dbContext) : base(dbContext)
    {
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.ToDo", cancellationToken);
    }
}
