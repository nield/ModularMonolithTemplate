using static ModularMonolithTemplate.Api.Common.Constants;

namespace ModularMonolithTemplate.Api.Configurations;

internal static class HeaderPropagation
{
    internal static void ConfigureHeaderPropagation(this IServiceCollection services)
    {
        services.AddHeaderPropagation(options =>
            options.Headers.Add(Headers.CorrelationId, context => context.HttpContext.GetCorrelationId()));
    }
}
