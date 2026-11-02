The C# parser maps Roslyn syntax nodes to the parser-independent `ParsedElement` tree. Roslyn types remain inside the C# parser; consumers receive a `ParseResult` with a common root and normalized diagnostics.

For the shared node fields and structural contract, see [Parser Output Structure](../Common/ParserOutputStructure.md).

| Roslyn node | `ElementKind` | Common information extracted |
|---|---|---|
| `CompilationUnitSyntax` | `CompilationUnit` | Root of the parsed file |
| `UsingDirectiveSyntax` | `Import` | Imported name and optional alias |
| `ClassDeclarationSyntax` | `Class` | Declaration name |
| `MethodDeclarationSyntax` | `Callable` | Name, return type, modifiers, parameters and body |
| `ParameterSyntax` | `Parameter` | Name, type and nullable status |
| `InvocationExpressionSyntax` | `Invocation` | Invoked expression and arguments as children |
| `BinaryExpressionSyntax` | `BinaryExpression` | Operator |
| `IfStatementSyntax` | `If` | Condition and branches as children |