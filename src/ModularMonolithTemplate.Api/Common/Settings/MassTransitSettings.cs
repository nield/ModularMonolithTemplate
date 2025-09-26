using System.Diagnostics.CodeAnalysis;

namespace ModularMonolithTemplate.Api.Common.Settings;

[ExcludeFromCodeCoverage]
public class MassTransitSettings
{
    public bool PublishEnabled { get; set; }
}