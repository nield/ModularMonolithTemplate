using ModularMonolithTemplate.Api.Tests.Helpers;
using Mono.Cecil;
using NetArchTest.Rules;

namespace ModularMonolithTemplate.Api.Tests.CustomRules;

public class OnlyAllowPublicModuleAccessRule : ICustomRule
{
    private const string _modules = "Modules";

    private readonly string _currentModuleNamespace;

    public OnlyAllowPublicModuleAccessRule(string currentModuleNamespace)
    {
        _currentModuleNamespace = currentModuleNamespace;
    }

    public bool MeetsRule(TypeDefinition type)
    {
        if (type.Namespace.Contains(_modules))
        {
            var referencedNamespaces = type.GetReferencedNamespaces();

            var nonPublicAccessToOtherModules = referencedNamespaces.Where(x => x.Contains(_modules)
                                                            && !x.Contains(_currentModuleNamespace)
                                                            && !x.EndsWith("public", StringComparison.OrdinalIgnoreCase));
            return !nonPublicAccessToOtherModules.Any();
        }

        return true;
    }
}
