using Microsoft.Extensions.Logging;
using VerticalSliceTemplate.Api.Common.Interfaces;
using VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;

namespace VerticalSliceTemplate.Api.Tests.Modules.Reminder;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected T Instance;

    protected readonly ILogger<T> _logger = Substitute.For<ILogger<T>>();

    protected BaseTestFixture()
    {
        Instance = CreateInstance();
    }

    protected abstract T CreateInstance();
}

public abstract class BaseTestFixture
{
    protected readonly IReminderDbContext _reminderDbContextMock = Substitute.For<IReminderDbContext>();
    protected readonly ICurrentUserService _currentUserServiceMock = Substitute.For<ICurrentUserService>();
    protected readonly IToDoRepository _toDoRepositoryMock = Substitute.For<IToDoRepository>();
    protected readonly IPublishMessageService _publishMessageService = Substitute.For<IPublishMessageService>();
}