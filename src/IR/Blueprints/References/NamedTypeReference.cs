using IR.Types;

namespace IR.References;

public class NamedTypeReference : TypeReference
{
    public NamedTypeReference(string name, string? @namespace = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Namespace = @namespace;
    }

    public string Name { get; }

    public string? Namespace { get; }

    public CompoundTypeBlueprint? ResolvedType { get; set; }
}
