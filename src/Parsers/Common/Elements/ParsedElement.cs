namespace Parsers;

public class ParsedElement(ElementKind kind)
{
    public ElementKind Kind { get; } = kind;
    public SourceSpan? Span { get; set; }
    public string Text { get; set; } = string.Empty;
    public List<ParsedElement> Children { get; } = new();
}
