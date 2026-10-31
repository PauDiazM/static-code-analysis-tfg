namespace Parsers;

public abstract class RuleSet<TNode>
    where TNode : class
{
    protected Dictionary<Type, IRule> Rules { get; } = new();

    public abstract List<TNode> Apply(ParsedElement element, TNode syntaxNode);
}