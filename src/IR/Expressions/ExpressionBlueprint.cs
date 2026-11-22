using IR.References;

namespace IR.Expressions;

public abstract class ExpressionBlueprint : Blueprint
{
    protected ExpressionBlueprint()
    {
    }

    public TypeReference? ResultType { get; set; }
}