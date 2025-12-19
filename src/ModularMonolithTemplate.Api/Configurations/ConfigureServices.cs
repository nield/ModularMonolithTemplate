using System.Diagnostics;
using ModularMonolithTemplate.Api.Common.Services;
namespace ModularMonolithTemplate.Api.Configurations;

internal static class ConfigureServices
{
    internal static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.ConfigureModules();

        builder.Services.Configure<RouteHandlerOptions>(options =>
            options.ThrowOnBadRequest = true);

        builder.Services.ConfigureCommonServices();

        builder.Services.AddOpenApi();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureFluentValidator();

        builder.Services.ConfigureExceptionHandlers();

        builder.Services.ConfigureSwagger();

        builder.Services.ConfigureVersioning();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureCompression();

        builder.Services.ConfigureHeaderPropagation();

        builder.Services.ConfigureHttpClients(config);

        builder.Services.ConfigureMassTransit(config);

        builder.SetupDatabase();

        builder.Services.AddProblemDetails(options => 
            options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
       
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                context.ProblemDetails.Extensions.TryAdd("traceId", Activity.Current?.TraceId);

                // Include invalid request details for bad requests
                if (context.Exception is BadHttpRequestException ex)
                {
                    context.ProblemDetails.Title = "Invalid request";
                    context.ProblemDetails.Detail = ex.GetFullErrorMessage();
                    context.ProblemDetails.Status = StatusCodes.Status400BadRequest;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            });

        return builder;
    }

    private static void ConfigureCommonServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
