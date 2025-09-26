using Types = NetArchTest.Rules.Types;

namespace ModularMonolithTemplate.Api.Tests.Modules.Reminder;

public class ArchitectureTests
{
    [Fact]
    public void Endpoints_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("ModularMonolithTemplate.Api.Modules.Reminder.Endpoints")
            .ShouldNot()
            .HaveDependencyOn("ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure")
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }

    [Fact]
    public void Endpoints_ShouldBeSealed()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("VerticalTemplate.Api.Modules.Reminder.Endpoints")
            .Should()
            .BeSealed()
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
}
