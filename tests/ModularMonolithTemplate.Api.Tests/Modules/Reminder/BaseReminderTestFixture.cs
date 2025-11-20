using Microsoft.Extensions.Logging;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;

namespace ModularMonolithTemplate.Api.Tests.Modules.Reminder;

public abstract class BaseReminderTestFixture<T> : BaseTestFixture where T : class
{
    protected T Instance;

    protected readonly ILogger<T> _logger = Substitute.For<ILogger<T>>();


    protected BaseReminderTestFixture()
    {
        Instance = CreateInstance();
    }

    protected abstract T CreateInstance();
}

public abstract class BaseReminderTestFixture : BaseTestFixture
{
    protected readonly IReminderQueryDbContext _reminderDbContextMock = Substitute.For<IReminderQueryDbContext>();
    protected readonly IToDoRepository _toDoRepositoryMock = Substitute.For<IToDoRepository>();
}
