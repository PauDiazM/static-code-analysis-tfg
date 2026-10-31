using Parsers;

public abstract class ExpressionElement : ParsedElement
{
    protected override void PrintSpecificInformation(string indent)
    {
        PrintExpressionInformation(indent);
    }
    protected virtual void PrintExpressionInformation(string indent){}
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
    protected override void PrintExpressionInformation(string indent)
    {
        Console.WriteLine($"{indent}Operator: {Operator}");
    }
}

public class IdentifierElement : ExpressionElement
{
    public string Name { get; set; } = string.Empty;
    protected override void PrintExpressionInformation(string indent)
    {
        Console.WriteLine($"{indent}Name: {Name}");
    }
}

public class LiteralElement : ExpressionElement
{
    public string Value { get; set; } = string.Empty;

    protected override void PrintExpressionInformation(string indent)
    {
        Console.WriteLine($"{indent}Value: {Value}");
    }
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

public class IsExpressionElement : ExpressionElement
{
    public string Pattern { get; set; } = string.Empty; 
    protected override void PrintExpressionInformation(string indent)
    {
        Console.WriteLine($"{indent}Pattern: {Pattern}");
    }
}