using Microsoft.CodeAnalysis;

namespace Parsers;

public abstract class RuleSet<TNode>
    where TNode : class
{
    protected Dictionary<Type, IRule> Rules { get; } = new();

    public List<TNode> Apply(ParsedElement element, TNode syntaxNode)
    {
        List<TNode> children = new();
        if (Rules.TryGetValue(element.GetType(), out var rule))
        {
            rule.ApplyRule(element, syntaxNode, children);
        }

        return children;
    }
}