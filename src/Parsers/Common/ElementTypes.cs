namespace Parsers;

public enum ElementCategory
{
    Declaration,
    Callable,
    Member,
    StatementExpression
}

public enum ElementKind
{
    Unknown,
    Using,
    Namespace,
    Identifier,
    PredefinedType,
    ParameterList,
    Block,
    ExpressionStatement,
    MemberAccess,
    ArgumentList,
    Argument,
    Literal,
    Class,
    Struct,
    Interface,
    Method,
    Constructor,
    Field,
    Property,
    Parameter,
    Invocation,
    If,
    Return
}