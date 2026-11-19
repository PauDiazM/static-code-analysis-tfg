namespace IR.Expressions;

public class BinaryExpressionBlueprint : ExpressionBlueprint
{
    public BinaryExpressionBlueprint(BinaryOperatorKind operatorKind) => OperatorKind = operatorKind;

    public ExpressionBlueprint? LeftOperand { get; set; }

    public BinaryOperatorKind OperatorKind { get; }

    public ExpressionBlueprint? RightOperand { get; set; }

}
