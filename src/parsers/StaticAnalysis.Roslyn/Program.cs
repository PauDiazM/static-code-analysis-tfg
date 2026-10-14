using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string source = File.ReadAllText("../../../code_samples/examples/HelloWorld.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(source);
SyntaxNode root = tree.GetRoot();

Console.WriteLine("=== SYNTAX TREE ===");
Console.WriteLine(root.ToFullString());

Console.WriteLine("\n=== STRUCTURE ===");
PrintTree(root);

Console.WriteLine("\n=== CLASSES ===");

foreach (var node in root.DescendantNodes()
                         .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>())
{
    Console.WriteLine($"Class: {node.Identifier.Text}");
}

Console.WriteLine("\n=== METHODS ===");

foreach (var node in root.DescendantNodes()
                         .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax>())
{
    Console.WriteLine($"Method: {node.Identifier.Text}");
}

Console.WriteLine("\n=== DIAGNOSTICS ===");

foreach (Diagnostic diagnostic in tree.GetDiagnostics())
{
    Console.WriteLine(diagnostic);
}



static void PrintTree(SyntaxNode node, int depth = 0)
{
    string details = node switch
    {
        // Namespaces and types
        NamespaceDeclarationSyntax n =>
            $"name={n.Name}",

        FileScopedNamespaceDeclarationSyntax n =>
            $"name={n.Name}",

        ClassDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        StructDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        InterfaceDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        EnumDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        RecordDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        // Methods and constructors
        MethodDeclarationSyntax n =>
            $"name={n.Identifier.Text}, returnType={n.ReturnType}",

        ConstructorDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        DestructorDeclarationSyntax n =>
            $"name={n.Identifier.Text}",

        LocalFunctionStatementSyntax n =>
            $"name={n.Identifier.Text}, returnType={n.ReturnType}",

        // Fields, properties and events
        FieldDeclarationSyntax n =>
            $"modifiers={n.Modifiers}, " +
            $"type={n.Declaration.Type}, " +
            $"names={string.Join(", ", n.Declaration.Variables.Select(v => v.Identifier.Text))}",

        PropertyDeclarationSyntax n =>
            $"name={n.Identifier.Text}, type={n.Type}",

        EventFieldDeclarationSyntax n =>
            $"type={n.Declaration.Type}, " +
            $"names={string.Join(", ", n.Declaration.Variables.Select(v => v.Identifier.Text))}",

        // Variables and parameters
        VariableDeclarationSyntax n =>
            $"type={n.Type}, " +
            $"names={string.Join(", ", n.Variables.Select(v => v.Identifier.Text))}",

        VariableDeclaratorSyntax n =>
            $"name={n.Identifier.Text}" +
            (n.Initializer is null ? "" :
                $", initializer={n.Initializer.Value}"),

        ParameterSyntax n =>
            $"name={n.Identifier.Text}, type={n.Type?.ToString() ?? "(inferred or implicit)"}",

        // Expressions and statements
        InvocationExpressionSyntax n =>
            $"expression={n.Expression}",

        ObjectCreationExpressionSyntax n =>
            $"type={n.Type}",

        AssignmentExpressionSyntax n =>
            $"operator={n.OperatorToken.Text}",

        BinaryExpressionSyntax n =>
            $"operator={n.OperatorToken.Text}",

        LiteralExpressionSyntax n =>
            $"value={n.Token.ValueText}",

        IdentifierNameSyntax n =>   
            $"name={n.Identifier.Text}",

        PredefinedTypeSyntax n =>
            $"type={n.Keyword.Text}",

        IfStatementSyntax =>
            "conditional branch",

        ReturnStatementSyntax n =>
            n.Expression is null ? "" : $"expression={n.Expression}",

        _ => ""
    };

    string suffix = string.IsNullOrEmpty(details)
        ? ""
        : $" [{details}]";

    Console.WriteLine(
        $"{new string(' ', depth * 2)}{node.Kind()}{suffix}");

    foreach (SyntaxNode child in node.ChildNodes())
    {
        PrintTree(child, depth + 1);
    }
}