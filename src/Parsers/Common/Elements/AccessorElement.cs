namespace Parsers;

public class AccessorElement() : ParsedElement(ElementKind.Accessor)
{
    public string Keyword { get; set; } = string.Empty;
    public List<string> Modifiers { get; set; } = new();
}