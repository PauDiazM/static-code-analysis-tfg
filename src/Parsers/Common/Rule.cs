namespace Parsers;

using System.Collections;

public abstract class Rule<TElement, TNode> : IRule<TElement, TNode>
    where TElement : ParsedElement
    where TNode : class
{
    public abstract void ApplyRule(
        TElement element, 
        TNode syntaxNode,
        List<TNode>? children = null);

    /// <summary>
    /// Implements the non-generic rule method by checking that the parsed element
    /// and syntax node match the expected generic types before applying the rule.
    /// </summary>
    void IRule.ApplyRule(
        ParsedElement element,
        object syntaxNode,
        IList? children)
    {
        if (element is TElement typedElement &&
            syntaxNode is TNode typedNode &&
            (children is null || children is List<TNode>))
        {
            ApplyRule(
                typedElement,
                typedNode,
                children as List<TNode>);
        }
    }
}