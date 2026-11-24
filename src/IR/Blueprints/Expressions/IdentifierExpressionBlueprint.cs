namespace IR.Expressions;

public class IdentifierExpressionBlueprint : ExpressionBlueprint
{
    public IdentifierExpressionBlueprint(string identifier)
    {
        Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
    }

    public string Identifier { get; }

    public Blueprint? Declaration { get; set; }
}
