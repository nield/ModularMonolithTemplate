using Microsoft.Extensions.Logging;
using ModularMonolithTemplate.Api.Common.Interfaces;

namespace ModularMonolithTemplate.Api.Tests;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    private readonly Lazy<T> _lazyInstance;

    protected readonly ILogger<T> Logger = Substitute.For<ILogger<T>>();

    protected BaseTestFixture()
    {
        _lazyInstance = new Lazy<T>(CreateInstance);
    }

    protected abstract T CreateInstance();

    protected T Instance => _lazyInstance.Value;
}

public abstract class BaseTestFixture
{
    protected readonly ICurrentUserService CurrentUserServiceMock = Substitute.For<ICurrentUserService>();
    protected readonly IPublishMessageService PublishMessageServiceMock = Substitute.For<IPublishMessageService>();
}