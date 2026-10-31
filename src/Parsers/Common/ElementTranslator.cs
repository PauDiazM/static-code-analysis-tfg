using Microsoft.CodeAnalysis.CSharp.Syntax;
using Parsers;

public class ElementTranslator : IElementTranslator
{
    public ParsedElement Translate(ElementKind kind)
    {
        return kind switch
        {
            ElementKind.Import => new ImportElement(),
            ElementKind.Class => new ClassElement(),
            ElementKind.Interface => new ContractElement(),
            ElementKind.Callable => new CallableElement(),
            ElementKind.Invocation => new InvocationElement(),
            ElementKind.BinaryExpression => new BinaryExpressionElement(),
            ElementKind.Identifier => new IdentifierElement(),
            ElementKind.Literal => new LiteralElement(),
            ElementKind.Parameter => new ParameterElement(),
            ElementKind.Argument => new ArgumentElement(),
            ElementKind.ArgumentList => new ArgumentElementList(),
            ElementKind.ParameterList => new ParameterElementList(),
            ElementKind.Block => new BlockElement(),
            ElementKind.Namespace => new NamespaceElement(),
            ElementKind.UnaryExpression => new UnaryExpressionElement(),
            ElementKind.TypeArgumentList => new TypeArgumentListElement(),
            ElementKind.InheritanceList => new InheritanceListElement(),
            ElementKind.InheritedType => new InheritedTypeElement(),

            _ => new UnknownElement(kind)
        };
    }
}