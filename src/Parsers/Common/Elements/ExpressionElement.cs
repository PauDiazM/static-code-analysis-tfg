namespace Parsers;

public class IdentifierElement() : ParsedElement(ElementKind.Identifier)
{
    public string Name { get; set; } = string.Empty;
}

public class LiteralElement() : ParsedElement(ElementKind.Literal)
{
    public string Value { get; set; } = string.Empty;
}

public class OperatorElement(ElementKind kind) : ParsedElement(kind)
{
    public string Operator { get; set; } = string.Empty;
}

public class IsExpressionElement() : ParsedElement(ElementKind.IsExpression)
{
    public string Pattern { get; set; } = string.Empty;
}