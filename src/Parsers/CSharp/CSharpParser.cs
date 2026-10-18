namespace Parsers;

using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class CSharpParser : IParser
{
    private readonly string _filepath;

    private static readonly Dictionary<string, ElementKind> Translator = new()
    {
        [typeof(UsingDirectiveSyntax).Name] = ElementKind.Using,
        [typeof(IdentifierNameSyntax).Name] = ElementKind.Identifier,
        [typeof(ClassDeclarationSyntax).Name] = ElementKind.Class,
        [typeof(MethodDeclarationSyntax).Name] = ElementKind.Method,
        [typeof(PredefinedTypeSyntax).Name] = ElementKind.PredefinedType,
        [typeof(ParameterListSyntax).Name] = ElementKind.ParameterList,
        [typeof(BlockSyntax).Name] = ElementKind.Block,
        [typeof(ExpressionStatementSyntax).Name] = ElementKind.ExpressionStatement,
        [typeof(InvocationExpressionSyntax).Name] = ElementKind.Invocation,
        [typeof(MemberAccessExpressionSyntax).Name] = ElementKind.MemberAccess,
        [typeof(ArgumentListSyntax).Name] = ElementKind.ArgumentList,
        [typeof(ArgumentSyntax).Name] = ElementKind.Argument,
        [typeof(LiteralExpressionSyntax).Name] = ElementKind.Literal
    };
    public CSharpParser(string filepath)
    {
        _filepath = filepath;
    }

    public void Parse()
    {

        string source = File.ReadAllText(_filepath);

        SyntaxTree tree = CSharpSyntaxTree.ParseText(source);
        SyntaxNode root = tree.GetRoot();

        List<ParsedElement> parsedElements = new List<ParsedElement>();

        int skipNodes = 0;
        foreach(var node in root.DescendantNodes())
        {
            if(skipNodes > 0)
            {
                skipNodes--;
                continue;
            }
            ParsedElement element = ParseNode(node);
            parsedElements.Add(element);
            skipNodes += CountChildElementsRecursive(element);
        }

        ShowParsedElements(tree, root, parsedElements);

    }

    public void ShowParsedElements(SyntaxTree tree, SyntaxNode root, List<ParsedElement>? parsedElements = null)
    {
        foreach(var node in root.DescendantNodes())
        {
            Console.WriteLine($"Node: {node.GetType().Name}");
            Console.WriteLine($"Text: {node.ToFullString()}");
            Console.WriteLine($"{node.ChildNodes().Count()} children");
        }

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

        if (parsedElements is not null)
        {
            Console.WriteLine("\n=== PARSED ELEMENTS ===");
            foreach (var element in parsedElements)
            {
                PrintElementTreeRecursive(element);
            }
        }

    }
    public void PrintElementTreeRecursive(ParsedElement element, int depth = 0)
    {
        
        Console.WriteLine($"{new string(' ', depth * 2)}Element: {element.Kind}, Name: {element.Name}, Type: {element.Type}, Operator: {element.Operator}, Children: {element.Children.Count}");
        foreach (var child in element.Children)
        {
            PrintElementTreeRecursive(child, depth + 1);
        }
    }
    private ParsedElement ParseNode(SyntaxNode node)
    {
        var element = new ParsedElement
        {
            Kind = Translator.TryGetValue(node.GetType().Name, out var kind) ? kind : ElementKind.Unknown,
            Name = kind switch
            {
                ElementKind.Class => ((ClassDeclarationSyntax)node).Identifier.Text,
                ElementKind.Method => ((MethodDeclarationSyntax)node).Identifier.Text,
                ElementKind.Parameter => ((ParameterSyntax)node).Identifier.Text,
                ElementKind.Identifier => ((IdentifierNameSyntax)node).Identifier.Text,
                _ => null
            },
            Type = kind switch
            {
                ElementKind.Method => ((MethodDeclarationSyntax)node).ReturnType.ToString(),
                ElementKind.Parameter => ((ParameterSyntax)node).Type?.ToString(),
                _ => null
            },
            Operator = kind switch
            {
                ElementKind.Invocation => ((InvocationExpressionSyntax)node).Expression.ToString(),
                _ => null
            },
            Text = node.ToFullString()
        };

        foreach (var child in node.ChildNodes())
        {
            element.Children.Add(ParseNode(child));
        }

        return element;
    }
    
    private int CountChildElementsRecursive(ParsedElement element)
    {
        int count = element.Children.Count;
        foreach (var child in element.Children)
        {
            count += CountChildElementsRecursive(child);
        }
        return count;
    }

    static private void PrintTree(SyntaxNode node, int depth = 0)
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
}