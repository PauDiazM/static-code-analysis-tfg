namespace Parsers.Roslyn;

using Microsoft.CodeAnalysis;
using Parsers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection.Metadata;

public class UnknownElementRule : Rule<UnknownElement, SyntaxNode>
{
    public override void ApplyRule(
        UnknownElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (children is null) return;

        children.AddRange(syntaxNode.ChildNodes());
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
        // This node contains the namespace's name

        if (syntaxNode is not UsingDirectiveSyntax usingSyntax)
            return;

        element.KeyWord = "using";

        if (children is null)
            return;
        children.AddRange(usingSyntax.ChildNodes());
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

        children.AddRange(classSyntax.ChildNodes());
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
        if (syntaxNode is IdentifierNameSyntax identifierSyntax)
            element.Name = identifierSyntax.Identifier.ValueText; 
        
        if (syntaxNode is SingleVariableDesignationSyntax singleVariableDesignationSyntax)
            element.Name = singleVariableDesignationSyntax.Identifier.ValueText;
        
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

        if (children is null)
            return;

        children.AddRange(literalSyntax.ChildNodes());
    }
}

public class ParameterElementRule : Rule<ParameterElement, SyntaxNode>
{
    public override void ApplyRule(
        ParameterElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not ParameterSyntax parameterSyntax)
            return;

        element.Name = parameterSyntax.Identifier.ValueText;
        element.Type = parameterSyntax.Type is not null ? parameterSyntax.Type.ToString() : "";
        element.IsNullable = parameterSyntax.Type is NullableTypeSyntax;

        if (children is null)
            return;

        children.AddRange(parameterSyntax.ChildNodes());
    }
}

public class IsExpressionElementRule : Rule<IsExpressionElement, SyntaxNode>
{
    public override void ApplyRule(
        IsExpressionElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not IsPatternExpressionSyntax isExpressionSyntax)
            return;

        element.Pattern = isExpressionSyntax.Pattern.ToString();

        if (children is null)
            return;
            
        children.AddRange(isExpressionSyntax.ChildNodes());
    }
}

public class ConstantElementRule : Rule<ConstantElement, SyntaxNode>
{
    public override void ApplyRule(
        ConstantElement element,
        SyntaxNode syntaxNode,
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not ConstantPatternSyntax constantPatternSyntax)
            return;


        if (children is null)
            return;
        
        children.Add(constantPatternSyntax.Expression);
    }
}

public class UnaryExpressionElementRule() : Rule<UnaryExpressionElement, SyntaxNode>
{
    public override void ApplyRule(
        UnaryExpressionElement element, 
        SyntaxNode syntaxNode, 
        List<SyntaxNode>? children)
    {
        if (syntaxNode is not UnaryPatternSyntax unaryPatternSyntax)
            return;

        element.Operator = unaryPatternSyntax.OperatorToken.ToString();

        if (children is null)
            return;
           
        children.AddRange(unaryPatternSyntax.ChildNodes());
    }
}