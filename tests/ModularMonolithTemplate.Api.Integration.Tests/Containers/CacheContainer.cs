using System.Diagnostics.CodeAnalysis;
using DotNet.Testcontainers.Builders;

namespace ModularMonolithTemplate.Api.Integration.Tests.Containers;

[ExcludeFromCodeCoverage]
internal sealed class CacheContainer : BaseContainer<CacheContainer>
{
    private const ushort CacheDefaultPort = 6379;
    
    protected override IContainer BuildContainer()
    {
        return new ContainerBuilder("redis:latest")
           .WithPortBinding(CacheDefaultPort, true)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(CacheDefaultPort))
           .Build();
    }

    public override string GetConnectionString() =>
        $"{Container.Hostname}:{Container.GetMappedPublicPort(CacheDefaultPort)}";
}