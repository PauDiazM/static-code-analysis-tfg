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
    }
}