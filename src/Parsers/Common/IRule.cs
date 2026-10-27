namespace Parsers;

using System.Collections;

/// <summary>
/// Common interface that allows the execution of rules
/// indenpendently of it's specific types.
/// </summary>
public interface IRule
{
    void ApplyRule(
        ParsedElement element, 
        object syntaxNode,
        IList? children = null);
}

/// <summary>
/// Defines a rule that operates on a specific type of parsed element
/// and a specific type of syntax node.
/// </summary>
/// <typeparam name="TElement">The type of parsed element the rule processes.</typeparam>
/// <typeparam name="TNode">The type of syntax node the rule uses to extract information.</typeparam>
public interface IRule<TElement, TNode> : IRule
    where TElement : ParsedElement
    where TNode : class
{
    void ApplyRule(
        TElement element, 
        TNode syntaxNode, 
        List<TNode>? children = null);
}
