namespace Parsers;

public class ParsedElement
{
    public ElementKind Kind { get; init; }
    public string? Name { get; init; }
    public string? Type { get; init; }
    public string? Operator { get; init; }
    public string? Text { get; init; }
    public List<ParsedElement> Children { get; init; } = new List<ParsedElement>();

}