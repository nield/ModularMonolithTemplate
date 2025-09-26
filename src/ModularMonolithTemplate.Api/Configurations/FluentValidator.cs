using System.Reflection;

namespace ModularMonolithTemplate.Api.Configurations;

internal static class FluentValidator
{
    internal static void ConfigureFluentValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(
        [
            Assembly.GetExecutingAssembly()
        ], ServiceLifetime.Singleton);
    }
}
