namespace Parsers;

public class DeclarationElement(ElementKind kind)
    : ParsedElement(kind)
{
    public string Name { get; set; } = string.Empty;

    private List<string> _modifiers { get; } = [];

    public IReadOnlyList<string> Modifiers => _modifiers;

    public void AddModifier(string modifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifier);
        _modifiers.Add(modifier);
    }
}

public class CallableElement() : DeclarationElement(ElementKind.Callable)
{
    public string? ReturnType { get; set; }
}

public class ParameterElement() : DeclarationElement(ElementKind.Parameter)
{
    public string Type { get; set; } = string.Empty;
    public bool IsNullable { get; set; }
}

public class ImportElement() : ParsedElement(ElementKind.Import)
{
    public string ImportedName { get; set; } = string.Empty;
    public string? Alias { get; set; }
    public bool IsGlobal { get; set; }
    public bool IsStatic { get; set; }
}

public class VariableElement() : DeclarationElement(ElementKind.VariableDeclarator)
{
    public string Type { get; set; } = string.Empty;
    public bool IsNullable { get; set; }
}

public class PropertyElement() : DeclarationElement(ElementKind.Property)
{
    public string Type { get; set; } = string.Empty;
    public bool IsNullable { get; set; }
}