using IR.Types;

namespace IR.Expressions;

public abstract class ExpressionBlueprint : Blueprint
{
    protected ExpressionBlueprint()
    {
    }

    public TypeBlueprint? ResultType { get; private set; }

    public void SetResultType(TypeBlueprint? resultType) => ResultType = resultType;
}