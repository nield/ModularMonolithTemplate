using VerticalSliceTemplate.Api.Common.Infrastructure.Persistance;
using VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;
using VerticalSliceTemplate.Api.Modules.Reminder.Entities;

namespace VerticalSliceTemplate.Api.Modules.Reminder.Infrastructure.Persistance.Repositories;

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
