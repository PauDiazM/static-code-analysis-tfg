namespace IR.Expressions;

public class AssignmentExpressionBlueprint : ExpressionBlueprint
{
    public AssignmentExpressionBlueprint(AssignmentOperatorKind operatorKind) => OperatorKind = operatorKind;

    public ExpressionBlueprint? LeftOperand { get; set; }

    public AssignmentOperatorKind OperatorKind { get; }

    public ExpressionBlueprint? RightOperand { get; set; }
 
}
