namespace Parsers;

public sealed class ParseResult(ParsedElement root)
{
    public ParsedElement Root { get; } = root;
    public List<ParseDiagnostic> Diagnostics { get; } = new();
}