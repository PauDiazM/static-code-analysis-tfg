using Parsers;

public class ElementTranslator : IElementTranslator
{
    public ParsedElement Translate(ElementKind kind)
    {
        return kind switch
        {
            ElementKind.Import => new ImportElement(),
            ElementKind.Class or ElementKind.Struct or ElementKind.Template
                or ElementKind.Interface or ElementKind.Record or ElementKind.Enum
                or ElementKind.Namespace => new DeclarationElement(kind),
            ElementKind.Callable => new CallableElement(),
            ElementKind.Parameter => new ParameterElement(),
            ElementKind.Identifier => new IdentifierElement(),
            ElementKind.Literal => new LiteralElement(),
            ElementKind.BinaryExpression or ElementKind.PrefixUnaryExpression
                or ElementKind.PostfixUnaryExpression or ElementKind.UnaryExpression
                or ElementKind.Assignment => new OperatorElement(kind),
            ElementKind.IsExpression => new IsExpressionElement(),
            _ => new ParsedElement(kind)
        };
    }
}