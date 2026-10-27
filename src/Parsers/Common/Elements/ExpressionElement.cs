using Parsers;

public abstract class ExpressionElement : ParsedElement
{
}

public class InvocationElement : ExpressionElement
{
}

public class BinaryExpressionElement : ExpressionElement
{
    public string Operator { get; set; } = string.Empty;
}

public class UnaryExpressionElement : ExpressionElement
{
    public string Operator { get; set; } = string.Empty;
}

public class IdentifierElement : ExpressionElement
{
    public string Name { get; set; } = string.Empty;
}

public class LiteralElement : ExpressionElement
{
    public string Value { get; set; } = string.Empty;
}

public class MemberAccessElement : ExpressionElement
{
}

public class IndexAccessElement : ExpressionElement
{
}

public class AssignmentElement : ExpressionElement
{
    public string Operator { get; set; } = string.Empty;
}

public class ConditionalExpressionElement : ExpressionElement
{
}

public class ObjectCreationElement : ExpressionElement
{
}

