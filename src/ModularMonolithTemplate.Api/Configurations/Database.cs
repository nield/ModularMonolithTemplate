using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ModularMonolithTemplate.Api.Common.Infrastructure.Persistance.Interceptors;

namespace ModularMonolithTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class Database
{
    internal static void SetupDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        builder.Services.SetupRepositories();
    }

    private static void SetupRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<Program>()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());
    }
}
