namespace Parsers.Roslyn;

using Microsoft.CodeAnalysis;
using Parsers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class UnknownElementRule : Rule<UnknownElement, SyntaxNode>
{
    public override void ApplyRule(
        UnknownElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (children is null)
            return;

        switch (syntaxNode)
        {
            case ExpressionStatementSyntax expressionStatement:
                children.Add(expressionStatement.Expression);
                break;

            case ArgumentListSyntax argumentList:
                children.AddRange(argumentList.Arguments);
                break;

            case ParameterListSyntax parameterList:
                children.AddRange(parameterList.Parameters);
                break;

            case BlockSyntax block:
                children.AddRange(block.Statements);
                break;

            // Other UnknownElement nodes remain leaves.
        }
    }
}

public class ImportElementRule : Rule<ImportElement, SyntaxNode>
{
    public override void ApplyRule(
        ImportElement element, 
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        // Roslyn uses UsingDirectiveSyntax for import nodes
        // UsingDirectiveSyntax has 1 IdentifierNameSyntax child node
        // This node contains the namespace's name

        if (syntaxNode is not UsingDirectiveSyntax usingSyntax)
            return;

        element.KeyWord = "using";
        element.Package = usingSyntax.Name?.ToString() ?? string.Empty;

    }

}

public class ClassElementRule : Rule<ClassElement, SyntaxNode>
{
    public override void ApplyRule(
        ClassElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not ClassDeclarationSyntax classSyntax)
            return;

        element.Name = classSyntax.Identifier.ValueText;

        if (children is null)
            return;

        children.AddRange(classSyntax.Members);
    }
}

public class CallableElementRule : Rule<CallableElement, SyntaxNode>
{
    public override void ApplyRule(
        CallableElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not MethodDeclarationSyntax methodSyntax)
            return;

        element.Name = methodSyntax.Identifier.ValueText;

        element.Modifiers = methodSyntax.Modifiers
            .Select(modifier => modifier.ValueText)
            .ToList();

        element.ReturnType = methodSyntax.ReturnType.ToString();

        if (children is null)
            return;

        children.Add(methodSyntax.ParameterList);

        if (methodSyntax.Body is not null)
            children.Add(methodSyntax.Body);

        if (methodSyntax.ExpressionBody is not null)
            children.Add(methodSyntax.ExpressionBody);
    }
}

public class IdentifierElementRule : Rule<IdentifierElement, SyntaxNode>
{
    public override void ApplyRule(
        IdentifierElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not IdentifierNameSyntax identifierSyntax)
            return;

        element.Name = identifierSyntax.Identifier.ValueText;
    }
}

public class InvocationElementRule
    : Rule<InvocationElement, SyntaxNode>
{
    public override void ApplyRule(
        InvocationElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not InvocationExpressionSyntax invocationSyntax)
            return;

        if (children is null)
            return;

        children.Add(invocationSyntax.Expression);
        children.Add(invocationSyntax.ArgumentList);
    }
}

public class MemberAccessElementRule
    : Rule<MemberAccessElement, SyntaxNode>
{
    public override void ApplyRule(
        MemberAccessElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not MemberAccessExpressionSyntax memberSyntax)
            return;

        if (children is null)
            return;

        children.Add(memberSyntax.Expression);
        children.Add(memberSyntax.Name);
    }
}

public class ArgumentElementRule : Rule<ArgumentElement, SyntaxNode>
{
    public override void ApplyRule(
        ArgumentElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not ArgumentSyntax argumentSyntax)
            return;

        if (children is null)
            return;

        children.Add(argumentSyntax.Expression);
    }
}

public class LiteralElementRule : Rule<LiteralElement, SyntaxNode>
{
    public override void ApplyRule(
        LiteralElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not LiteralExpressionSyntax literalSyntax)
            return;

        element.Value = literalSyntax.Token.ValueText;
    }
}