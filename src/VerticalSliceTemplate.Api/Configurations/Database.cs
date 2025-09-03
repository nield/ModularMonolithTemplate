using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VerticalSliceTemplate.Api.Common.Infrastructure.Persistance.Interceptors;
using VerticalSliceTemplate.Api.Modules.Reminder.Common.Interfaces;
using VerticalSliceTemplate.Api.Modules.Reminder.Infrastructure;

namespace VerticalSliceTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class Database
{
    internal static void SetupDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        builder.Services.SetupRepositories();

        builder.SetupReminderDatabase();
    }

    private static void SetupRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IReminderDbContext>()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());
    }
}
