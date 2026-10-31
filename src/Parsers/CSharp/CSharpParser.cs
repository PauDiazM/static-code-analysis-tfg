namespace Parsers.Roslyn;

using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Parsers;

public class CSharpParser : IParser
{
    private readonly string _filepath;

    private readonly RoslynRuleSet _ruleSet = new();

    private static readonly Dictionary<Type, ElementKind> _nodeToElementKindTranslator = new()
    {
        // Compilation and namespaces
        [typeof(CompilationUnitSyntax)] = ElementKind.CompilationUnit,
        [typeof(UsingDirectiveSyntax)] = ElementKind.Import,
        [typeof(NamespaceDeclarationSyntax)] = ElementKind.Namespace,
        [typeof(FileScopedNamespaceDeclarationSyntax)] = ElementKind.Namespace,

        // Type declarations
        [typeof(ClassDeclarationSyntax)] = ElementKind.Class,
        [typeof(StructDeclarationSyntax)] = ElementKind.Struct,
        [typeof(InterfaceDeclarationSyntax)] = ElementKind.Interface,
        [typeof(RecordDeclarationSyntax)] = ElementKind.Record,
        [typeof(EnumDeclarationSyntax)] = ElementKind.Enum,

        // Members
        [typeof(MethodDeclarationSyntax)] = ElementKind.Callable,
        [typeof(ConstructorDeclarationSyntax)] = ElementKind.Callable,
        [typeof(DestructorDeclarationSyntax)] = ElementKind.Callable,
        [typeof(PropertyDeclarationSyntax)] = ElementKind.Property,
        [typeof(IndexerDeclarationSyntax)] = ElementKind.Indexer,
        [typeof(FieldDeclarationSyntax)] = ElementKind.Field,
        [typeof(EventDeclarationSyntax)] = ElementKind.Event,
        [typeof(DelegateDeclarationSyntax)] = ElementKind.Delegate,

        // Parameters and variables
        [typeof(ParameterListSyntax)] = ElementKind.ParameterList,
        [typeof(ParameterSyntax)] = ElementKind.Parameter,
        [typeof(VariableDeclarationSyntax)] = ElementKind.VariableDeclaration,
        [typeof(VariableDeclaratorSyntax)] = ElementKind.VariableDeclarator,
        [typeof(DeclarationPatternSyntax)] = ElementKind.VariableDeclaration,
        [typeof(ConstantPatternSyntax)] = ElementKind.ConstantDeclaration,
        [typeof(BaseListSyntax)] = ElementKind.InheritanceList,
        [typeof(SimpleBaseTypeSyntax)] = ElementKind.InheritedType,

        // Types
        [typeof(PredefinedTypeSyntax)] = ElementKind.PredefinedType,
        [typeof(IdentifierNameSyntax)] = ElementKind.Identifier,
        [typeof(SingleVariableDesignationSyntax)] = ElementKind.Identifier,
        [typeof(GenericNameSyntax)] = ElementKind.GenericName,
        [typeof(QualifiedNameSyntax)] = ElementKind.QualifiedName,
        [typeof(ArrayTypeSyntax)] = ElementKind.ArrayType,
        [typeof(NullableTypeSyntax)] = ElementKind.NullableType,

        // Statements
        [typeof(BlockSyntax)] = ElementKind.Block,
        [typeof(LocalDeclarationStatementSyntax)] = ElementKind.LocalDeclaration,
        [typeof(ExpressionStatementSyntax)] = ElementKind.ExpressionStatement,
        [typeof(ReturnStatementSyntax)] = ElementKind.Return,
        [typeof(ThrowStatementSyntax)] = ElementKind.Throw,
        [typeof(BreakStatementSyntax)] = ElementKind.Break,
        [typeof(ContinueStatementSyntax)] = ElementKind.Continue,
        [typeof(IfStatementSyntax)] = ElementKind.If,
        [typeof(ElseClauseSyntax)] = ElementKind.Else,
        [typeof(ForStatementSyntax)] = ElementKind.For,
        [typeof(ForEachStatementSyntax)] = ElementKind.ForEach,
        [typeof(WhileStatementSyntax)] = ElementKind.While,
        [typeof(DoStatementSyntax)] = ElementKind.DoWhile,
        [typeof(SwitchStatementSyntax)] = ElementKind.Switch,
        [typeof(SwitchSectionSyntax)] = ElementKind.SwitchSection,
        [typeof(TryStatementSyntax)] = ElementKind.Try,
        [typeof(CatchClauseSyntax)] = ElementKind.Catch,
        [typeof(FinallyClauseSyntax)] = ElementKind.Finally,

        // Expressions
        [typeof(InvocationExpressionSyntax)] = ElementKind.Invocation,
        [typeof(MemberAccessExpressionSyntax)] = ElementKind.MemberAccess,
        [typeof(ArgumentListSyntax)] = ElementKind.ArgumentList,
        [typeof(ArgumentSyntax)] = ElementKind.Argument,
        [typeof(TypeArgumentListSyntax)] = ElementKind.TypeArgumentList,
        [typeof(LiteralExpressionSyntax)] = ElementKind.Literal,
        [typeof(AssignmentExpressionSyntax)] = ElementKind.Assignment,
        [typeof(BinaryExpressionSyntax)] = ElementKind.BinaryExpression,
        [typeof(PrefixUnaryExpressionSyntax)] = ElementKind.PrefixUnaryExpression,
        [typeof(PostfixUnaryExpressionSyntax)] = ElementKind.PostfixUnaryExpression,
        [typeof(UnaryPatternSyntax)] = ElementKind.UnaryExpression,
        [typeof(ConditionalExpressionSyntax)] = ElementKind.ConditionalExpression,
        [typeof(ObjectCreationExpressionSyntax)] = ElementKind.ObjectCreation,
        [typeof(ImplicitObjectCreationExpressionSyntax)] = ElementKind.ImplicitObjectCreation,
        [typeof(ElementAccessExpressionSyntax)] = ElementKind.ElementAccess,
        [typeof(ParenthesizedExpressionSyntax)] = ElementKind.ParenthesizedExpression,
        [typeof(CastExpressionSyntax)] = ElementKind.Cast,
        [typeof(AwaitExpressionSyntax)] = ElementKind.Await,
        [typeof(ConditionalAccessExpressionSyntax)] = ElementKind.ConditionalAccess,
        [typeof(AnonymousMethodExpressionSyntax)] = ElementKind.AnonymousMethod,
        [typeof(SimpleLambdaExpressionSyntax)] = ElementKind.Lambda,
        [typeof(ParenthesizedLambdaExpressionSyntax)] = ElementKind.Lambda,
        [typeof(InterpolatedStringExpressionSyntax)] = ElementKind.InterpolatedString,
        [typeof(InterpolatedStringTextSyntax)] = ElementKind.InterpolatedText,
        [typeof(InterpolationSyntax)] = ElementKind.Interpolation,
        [typeof(IsPatternExpressionSyntax)] = ElementKind.IsExpression,

        // Initializers and arrays
        [typeof(InitializerExpressionSyntax)] = ElementKind.Initializer,
        [typeof(ArrayCreationExpressionSyntax)] = ElementKind.ArrayCreation,
        [typeof(ImplicitArrayCreationExpressionSyntax)] = ElementKind.ImplicitArrayCreation,

        // Other
        [typeof(ArrowExpressionClauseSyntax)] = ElementKind.ArrowExpression,
        [typeof(EqualsValueClauseSyntax)] = ElementKind.EqualsValue,
        [typeof(AttributeSyntax)] = ElementKind.Attribute,
        [typeof(AttributeListSyntax)] = ElementKind.AttributeList,
    };

    private static readonly ElementTranslator _elementKindToParsedElementTranslator = new();

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
            skipNodes += CountChildElementsRecursive(node);
        }

        ShowParsedElements(tree, root, parsedElements);

    }

    public void ShowParsedElements(SyntaxTree tree, SyntaxNode root, List<ParsedElement>? parsedElements = null)
    {
        foreach(var node in root.DescendantNodes())
        {
            Console.WriteLine($"Node: {node.GetType().Name}, Text: {node.ToFullString()}");
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
        element.PrintInformation(depth);
        foreach (var child in element.Children)
        {
            PrintElementTreeRecursive(child, depth + 1);
        }
    }
    private ParsedElement ParseNode(SyntaxNode node)
    {   
        _nodeToElementKindTranslator.TryGetValue(node.GetType(), out var kind);
        ParsedElement element = _elementKindToParsedElementTranslator.Translate(kind);

        element.Kind = kind;
        element.Text = node.ToFullString();

        var children = _ruleSet.Apply(element, node);
        
        foreach (var child in children)
        {
            element.Children.Add(ParseNode(child));
        }

        return element;
    }

    private int CountChildElementsRecursive(SyntaxNode node)
    {
        int count = node.ChildNodes().Count();
        foreach (var child in node.ChildNodes())
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
            $"{new string(' ', depth * 2)}{node.GetType().FullName}{suffix}");

        foreach (SyntaxNode child in node.ChildNodes())
        {
            PrintTree(child, depth + 1);
        }
    }



}