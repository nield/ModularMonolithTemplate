using Mono.Cecil;
using System.Diagnostics.CodeAnalysis;

namespace ModularMonolithTemplate.Api.Tests.Helpers;

[ExcludeFromCodeCoverage]
internal static class CecilExtensions
{
    // quick utility to gather namespaces from all references a type uses
    public static IEnumerable<string> GetReferencedNamespaces(this TypeDefinition type)
    {
        var namespaces = new HashSet<string>();

        // base + interfaces
        if (type.BaseType != null) namespaces.Add(type.BaseType.Namespace);
        foreach (var iface in type.Interfaces) namespaces.Add(iface.InterfaceType.Namespace);

        // fields
        foreach (var f in type.Fields) namespaces.Add(f.FieldType.Namespace);

        // properties
        foreach (var p in type.Properties) namespaces.Add(p.PropertyType.Namespace);

        // methods
        foreach (var m in type.Methods)
        {
            namespaces.Add(m.ReturnType.Namespace);
            foreach (var param in m.Parameters) namespaces.Add(param.ParameterType.Namespace);

            if (!m.HasBody) continue;
            foreach (var instr in m.Body.Instructions)
            {
                switch (instr.Operand)
                {
                    case TypeReference tr:
                        namespaces.Add(tr.Namespace);
                        break;
                    case MethodReference mr:
                        namespaces.Add(mr.DeclaringType.Namespace);
                        break;
                    case FieldReference fr:
                        namespaces.Add(fr.DeclaringType.Namespace);
                        break;
                }
            }
        }

        return namespaces.Where(n => n != null)!;
    }
}