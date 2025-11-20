using Microsoft.Extensions.Logging;
using ModularMonolithTemplate.Api.Common.Interfaces;

namespace ModularMonolithTemplate.Api.Tests.Modules.Reminder;

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
    protected readonly ICurrentUserService _currentUserServiceMock = Substitute.For<ICurrentUserService>();
    protected readonly IPublishMessageService _publishMessageService = Substitute.For<IPublishMessageService>();
}