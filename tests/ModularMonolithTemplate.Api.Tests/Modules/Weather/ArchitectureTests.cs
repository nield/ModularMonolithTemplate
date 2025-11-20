using ModularMonolithTemplate.Api.Tests.CustomRules;
using Types = NetArchTest.Rules.Types;

namespace ModularMonolithTemplate.Api.Tests.Modules.Weather;

public class ArchitectureTests
{
    private const string _namespace = "ModularMonolithTemplate.Api.Modules.Weather";
    
    [Fact]
    public void Endpoints_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("ModularMonolithTemplate.Api.Modules.Weather.Endpoints")
            .ShouldNot()
            .HaveDependencyOn("ModularMonolithTemplate.Api.Modules.Weather.Infrastructure")
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }

    [Fact]
    public void Endpoints_ShouldBeSealed()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("ModularMonolithTemplate.Api.Modules.Weather.Endpoints")
            .Should()
            .BeSealed()
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }

    [Fact]
    public void WeatherModule_ShouldNotHaveDependencyOn_OtherModulesNotPubliclyExposed()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace(_namespace)
            .Should()
            .MeetCustomRule(new OnlyAllowPublicModuleAccessRule(_namespace))
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}