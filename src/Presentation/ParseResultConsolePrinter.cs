using Parsers;

namespace Presentation;

public sealed class ParseResultConsolePrinter
{
    public void Print(ParseResult result)
    {
        Console.WriteLine("=== PARSED ELEMENTS ===");
        PrintElement(result.Root);

        if (result.Diagnostics.Count == 0)
        {
            return;
        }

        Console.WriteLine("\n=== PARSE DIAGNOSTICS ===");
        foreach (ParseDiagnostic diagnostic in result.Diagnostics)
        {
            string code = diagnostic.Code is null ? string.Empty : $" {diagnostic.Code}";
            string span = diagnostic.Span is null
                ? string.Empty
                : $" at {diagnostic.Span.StartOffset}+{diagnostic.Span.Length}";
            Console.WriteLine($"{diagnostic.Severity}{code}{span}: {diagnostic.Message}");
        }
    }

    private static void PrintElement(ParsedElement element, int depth = 0)
    {
        string indent = new(' ', depth * 2);
        string span = element.Span is null
            ? string.Empty
            : $" [{element.Span.StartOffset}..{element.Span.EndOffset})";
        string details = element switch
        {
            ImportElement import => $" {import.ImportedName}",
            CallableElement callable =>
                $" {callable.Name}({string.Join(" ", callable.Modifiers)})" +
                (callable.ReturnType is null ? string.Empty : $": {callable.ReturnType}"),
            ParameterElement parameter => $" {parameter.Name}: {parameter.Type}",
            DeclarationElement declaration => $" {declaration.Name}",
            IdentifierElement identifier => $" {identifier.Name}",
            LiteralElement literal => $" {literal.Value}",
            OperatorElement @operator => $" {@operator.Operator}",
            IsExpressionElement isExpression => $" {isExpression.Pattern}",
            AccessorElement accessorElement => $" {accessorElement.Keyword}",
            _ => string.Empty
        };

        Console.WriteLine($"{indent}{element.Kind}{span}{details}");
        foreach (ParsedElement child in element.Children)
        {
            PrintElement(child, depth + 1);
        }
    }
}
