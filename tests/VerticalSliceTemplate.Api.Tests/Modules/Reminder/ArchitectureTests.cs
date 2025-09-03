using Types = NetArchTest.Rules.Types;

namespace VerticalSliceTemplate.Api.Tests.Modules.Reminder;

public class ArchitectureTests
{
    [Fact]
    public void Endpoints_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("VerticalSliceTemplate.Api.Modules.Reminder.Endpoints")
            .ShouldNot()
            .HaveDependencyOn("VerticalSliceTemplate.Api.Modules.Reminder.Infrastructure")
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
