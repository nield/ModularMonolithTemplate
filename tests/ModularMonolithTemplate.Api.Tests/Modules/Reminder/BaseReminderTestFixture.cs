using Microsoft.Extensions.Logging;
using ModularMonolithTemplate.Api.Modules.Reminder.Common.Interfaces;

namespace ModularMonolithTemplate.Api.Tests.Modules.Reminder;

public abstract class BaseReminderTestFixture<T> : BaseTestFixture where T : class
{
    private readonly Lazy<T> _lazyInstance;

    protected readonly ILogger<T> Logger = Substitute.For<ILogger<T>>();


    protected BaseReminderTestFixture()
    {
        _lazyInstance = new Lazy<T>(CreateInstance);
    }

    protected abstract T CreateInstance();

    protected T Instance => _lazyInstance.Value;
}

public abstract class BaseReminderTestFixture : BaseTestFixture
{
    protected readonly IReminderQueryDbContext ReminderDbContextMock = Substitute.For<IReminderQueryDbContext>();
    protected readonly IToDoRepository ToDoRepositoryMock = Substitute.For<IToDoRepository>();
}
