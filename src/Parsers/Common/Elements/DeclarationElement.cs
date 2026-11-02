namespace Parsers;

public abstract class DeclarationElement(ElementKind kind) : ParsedElement(kind)
{
    public string Name { get; set; } = string.Empty;
}

public class CallableElement() : DeclarationElement(ElementKind.Callable)
{
    public List<string> Modifiers { get; set; } = new();
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
}