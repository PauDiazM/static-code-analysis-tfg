namespace Parsers.Roslyn;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Parsers;

internal sealed class CSharpElementMapper
{
    private static readonly Dictionary<Type, ElementKind> NodeKinds = new()
    {
        [typeof(CompilationUnitSyntax)] = ElementKind.CompilationUnit,
        [typeof(UsingDirectiveSyntax)] = ElementKind.Import,
        [typeof(NamespaceDeclarationSyntax)] = ElementKind.Namespace,
        [typeof(FileScopedNamespaceDeclarationSyntax)] = ElementKind.Namespace,
        [typeof(ClassDeclarationSyntax)] = ElementKind.Class,
        [typeof(StructDeclarationSyntax)] = ElementKind.Struct,
        [typeof(InterfaceDeclarationSyntax)] = ElementKind.Interface,
        [typeof(RecordDeclarationSyntax)] = ElementKind.Record,
        [typeof(EnumDeclarationSyntax)] = ElementKind.Enum,
        [typeof(EnumMemberDeclarationSyntax)] = ElementKind.EnumMember,
        [typeof(MethodDeclarationSyntax)] = ElementKind.Callable,
        [typeof(ConstructorDeclarationSyntax)] = ElementKind.Callable,
        [typeof(DestructorDeclarationSyntax)] = ElementKind.Callable,
        [typeof(LocalFunctionStatementSyntax)] = ElementKind.Callable,
        [typeof(PropertyDeclarationSyntax)] = ElementKind.Property,
        [typeof(AccessorListSyntax)] = ElementKind.AccessorList,
        [typeof(AccessorDeclarationSyntax)] = ElementKind.Accessor,
        [typeof(IndexerDeclarationSyntax)] = ElementKind.Indexer,
        [typeof(FieldDeclarationSyntax)] = ElementKind.Field,
        [typeof(EventDeclarationSyntax)] = ElementKind.Event,
        [typeof(DelegateDeclarationSyntax)] = ElementKind.Delegate,
        [typeof(ParameterListSyntax)] = ElementKind.ParameterList,
        [typeof(ParameterSyntax)] = ElementKind.Parameter,
        [typeof(VariableDeclarationSyntax)] = ElementKind.VariableDeclaration,
        [typeof(VariableDeclaratorSyntax)] = ElementKind.VariableDeclarator,
        [typeof(DeclarationPatternSyntax)] = ElementKind.VariableDeclaration,
        [typeof(ConstantPatternSyntax)] = ElementKind.ConstantDeclaration,
        [typeof(BaseListSyntax)] = ElementKind.InheritanceList,
        [typeof(SimpleBaseTypeSyntax)] = ElementKind.InheritedType,
        [typeof(PredefinedTypeSyntax)] = ElementKind.PredefinedType,
        [typeof(IdentifierNameSyntax)] = ElementKind.Identifier,
        [typeof(SingleVariableDesignationSyntax)] = ElementKind.Identifier,
        [typeof(GenericNameSyntax)] = ElementKind.GenericName,
        [typeof(QualifiedNameSyntax)] = ElementKind.QualifiedName,
        [typeof(ArrayTypeSyntax)] = ElementKind.ArrayType,
        [typeof(ArrayRankSpecifierSyntax)] = ElementKind.ArrayDimensionList,
        [typeof(OmittedArraySizeExpressionSyntax)] = ElementKind.ArrayDimension,
        [typeof(NullableTypeSyntax)] = ElementKind.NullableType,
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
        [typeof(InvocationExpressionSyntax)] = ElementKind.Invocation,
        [typeof(MemberAccessExpressionSyntax)] = ElementKind.MemberAccess,
        [typeof(ArgumentListSyntax)] = ElementKind.ArgumentList,
        [typeof(BracketedArgumentListSyntax)] = ElementKind.ArgumentList,
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
        [typeof(InitializerExpressionSyntax)] = ElementKind.Initializer,
        [typeof(ArrayCreationExpressionSyntax)] = ElementKind.ArrayCreation,
        [typeof(ImplicitArrayCreationExpressionSyntax)] = ElementKind.ImplicitArrayCreation,
        [typeof(ArrowExpressionClauseSyntax)] = ElementKind.ArrowExpression,
        [typeof(EqualsValueClauseSyntax)] = ElementKind.EqualsValue,
        [typeof(AttributeSyntax)] = ElementKind.Attribute,
        [typeof(AttributeListSyntax)] = ElementKind.AttributeList
    };

    private readonly ElementTranslator _elementTranslator = new();

    public ParsedElement Map(SyntaxNode syntaxNode)
    {
        ElementKind kind = NodeKinds.GetValueOrDefault(syntaxNode.GetType(), ElementKind.Unknown);
        ParsedElement element = _elementTranslator.Translate(kind);
        element.Span = new SourceSpan(syntaxNode.SpanStart, syntaxNode.Span.Length);
        element.Text = syntaxNode.ToString();

        PopulateElementData(element, syntaxNode);

        foreach (SyntaxNode child in GetChildren(syntaxNode))
        {
            element.Children.Add(Map(child));
        }

        return element;
    }

    private static void PopulateElementData(ParsedElement element, SyntaxNode syntaxNode)
    {
        switch (element)
        {
            case ImportElement import when syntaxNode is UsingDirectiveSyntax usingDirective:
                import.ImportedName = usingDirective.Name?.ToString() ?? string.Empty;
                import.Alias = usingDirective.Alias?.Name.Identifier.ValueText;
                break;

            case CallableElement callable:
                PopulateCallable(callable, syntaxNode);
                break;

            case ParameterElement parameter when syntaxNode is ParameterSyntax parameterSyntax:
                parameter.Name = parameterSyntax.Identifier.ValueText;
                parameter.Type = parameterSyntax.Type?.ToString() ?? string.Empty;
                parameter.IsNullable = parameterSyntax.Type is NullableTypeSyntax;
                break;

            case AccessorElement accessor when syntaxNode is AccessorDeclarationSyntax accessorSyntax:
                accessor.Keyword = accessorSyntax.Keyword.ValueText;
                accessor.Modifiers = accessorSyntax.Modifiers.Select(modifier => modifier.ValueText).ToList();
                break;

            case DeclarationElement declaration:
                declaration.Name = syntaxNode switch
                {
                    BaseNamespaceDeclarationSyntax namespaceDeclaration =>
                        namespaceDeclaration.Name.ToString(),
                    BaseTypeDeclarationSyntax typeDeclaration =>
                        typeDeclaration.Identifier.ValueText,
                    EnumMemberDeclarationSyntax enumMember =>
                        enumMember.Identifier.ValueText,
                    _ => string.Empty
                };
                break;

            case IdentifierElement identifier:
                identifier.Name = syntaxNode switch
                {
                    IdentifierNameSyntax identifierName => identifierName.Identifier.ValueText,
                    SingleVariableDesignationSyntax designation => designation.Identifier.ValueText,
                    _ => string.Empty
                };
                break;

            case LiteralElement literal when syntaxNode is LiteralExpressionSyntax literalSyntax:
                literal.Value = literalSyntax.Token.ValueText;
                break;

            case OperatorElement @operator:
                @operator.Operator = syntaxNode switch
                {
                    AssignmentExpressionSyntax assignment => assignment.OperatorToken.ValueText,
                    BinaryExpressionSyntax binary => binary.OperatorToken.ValueText,
                    PrefixUnaryExpressionSyntax prefix => prefix.OperatorToken.ValueText,
                    PostfixUnaryExpressionSyntax postfix => postfix.OperatorToken.ValueText,
                    UnaryPatternSyntax unaryPattern => unaryPattern.OperatorToken.ValueText,
                    _ => string.Empty
                };
                break;

            case IsExpressionElement isExpression
                when syntaxNode is IsPatternExpressionSyntax isPatternExpression:
                isExpression.Pattern = isPatternExpression.Pattern.ToString();
                break;
        }
    }

    private static void PopulateCallable(CallableElement callable, SyntaxNode syntaxNode)
    {
        switch (syntaxNode)
        {
            case MethodDeclarationSyntax method:
                callable.Name = method.Identifier.ValueText;
                callable.ReturnType = method.ReturnType.ToString();
                callable.Modifiers = method.Modifiers.Select(modifier => modifier.ValueText).ToList();
                break;
            case ConstructorDeclarationSyntax constructor:
                callable.Name = constructor.Identifier.ValueText;
                callable.Modifiers = constructor.Modifiers.Select(modifier => modifier.ValueText).ToList();
                break;
            case DestructorDeclarationSyntax destructor:
                callable.Name = destructor.Identifier.ValueText;
                break;
            case LocalFunctionStatementSyntax localFunction:
                callable.Name = localFunction.Identifier.ValueText;
                callable.ReturnType = localFunction.ReturnType.ToString();
                callable.Modifiers = localFunction.Modifiers.Select(modifier => modifier.ValueText).ToList();
                break;
        }
    }

    private static IEnumerable<SyntaxNode> GetChildren(SyntaxNode syntaxNode)
    {
        switch (syntaxNode)
        {
            case MethodDeclarationSyntax method:
                return CallableChildren(method.ParameterList, method.Body, method.ExpressionBody);
            case ConstructorDeclarationSyntax constructor:
                return CallableChildren(constructor.ParameterList, constructor.Body, constructor.ExpressionBody);
            case DestructorDeclarationSyntax destructor:
                return CallableChildren(destructor.ParameterList, destructor.Body, destructor.ExpressionBody);
            case LocalFunctionStatementSyntax localFunction:
                return CallableChildren(localFunction.ParameterList, localFunction.Body, localFunction.ExpressionBody);
            case InvocationExpressionSyntax invocation:
                return [invocation.Expression, invocation.ArgumentList];
            case MemberAccessExpressionSyntax memberAccess:
                return [memberAccess.Expression, memberAccess.Name];
            case ArgumentSyntax argument:
                return [argument.Expression];
            case ConstantPatternSyntax constantPattern:
                return [constantPattern.Expression];
            default:
                return syntaxNode.ChildNodes();
        }
    }

    private static IEnumerable<SyntaxNode> CallableChildren(
        ParameterListSyntax parameterList,
        BlockSyntax? body,
        ArrowExpressionClauseSyntax? expressionBody)
    {
        yield return parameterList;

        if (body is not null)
        {
            yield return body;
        }

        if (expressionBody is not null)
        {
            yield return expressionBody;
        }
    }
}
