namespace Parsers;

public class GenericNameElement() : ParsedElement(ElementKind.GenericName)
{
    private readonly List<ParsedElement> _argumentTypes = [];

    public string BaseType { get; set; } = string.Empty;

    public IReadOnlyList<ParsedElement> ArgumentTypes => _argumentTypes;

    public void AddArgumentType(ParsedElement argument)
    {
        ArgumentNullException.ThrowIfNull(argument);
        _argumentTypes.Add(argument);
    }
}