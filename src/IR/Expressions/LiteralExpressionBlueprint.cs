namespace IR.Expressions;

public class LiteralExpressionBlueprint : ExpressionBlueprint
{
    public LiteralExpressionBlueprint(object? value)
    {
        Value = value;
    }

    public object? Value { get; }
}
