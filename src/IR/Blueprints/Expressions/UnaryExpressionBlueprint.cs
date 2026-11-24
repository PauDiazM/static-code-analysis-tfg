namespace IR.Expressions;

public class UnaryExpressionBlueprint : ExpressionBlueprint
{
    public UnaryExpressionBlueprint(UnaryOperatorKind operatorKind) => OperatorKind = operatorKind;

    public UnaryOperatorKind OperatorKind { get; }

    public ExpressionBlueprint? Operand { get; private set; }

    public void SetOperand(ExpressionBlueprint operand) =>
        Operand = operand ?? throw new ArgumentNullException(nameof(operand));
}
