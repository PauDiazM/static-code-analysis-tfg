namespace Parsers.Roslyn;

using Microsoft.CodeAnalysis;
using Parsers;

public class RoslynRuleSet : RuleSet<SyntaxNode>
{
    public RoslynRuleSet()
    {
        Rules[typeof(UnknownElement)] = new UnknownElementRule();
        Rules[typeof(ImportElement)] = new ImportElementRule();
        Rules[typeof(ClassElement)] = new ClassElementRule();
        Rules[typeof(CallableElement)] = new CallableElementRule();
        Rules[typeof(IdentifierElement)] = new IdentifierElementRule();
        Rules[typeof(InvocationElement)] = new InvocationElementRule();
        Rules[typeof(MemberAccessElement)] = new MemberAccessElementRule();
        Rules[typeof(ArgumentElement)] = new ArgumentElementRule();
        Rules[typeof(LiteralElement)] = new LiteralElementRule();
        Rules[typeof(ParameterElement)] = new ParameterElementRule();
        Rules[typeof(IsExpressionElement)] = new IsExpressionElementRule();
        Rules[typeof(ConstantElement)] = new ConstantElementRule();
        Rules[typeof(UnaryExpressionElement)] = new UnaryExpressionElementRule();
    }

    public override List<SyntaxNode> Apply(ParsedElement element, SyntaxNode syntaxNode)
    {
        List<SyntaxNode> children = new();
        if (Rules.TryGetValue(element.GetType(), out var rule))
        {
            rule.ApplyRule(element, syntaxNode, children);
        } else
        {
            children.AddRange(syntaxNode.ChildNodes());
        }

        return children;
    }
}