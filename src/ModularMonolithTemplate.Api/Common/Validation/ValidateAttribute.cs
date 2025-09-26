using System.Diagnostics.CodeAnalysis;

namespace ModularMonolithTemplate.Api.Common.Validation;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{

}