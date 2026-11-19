using IR.Expressions;

namespace IR.Statements;

public class ConditionalStatementBlueprint : StatementBlueprint
{
    public ExpressionBlueprint? Condition { get; set; }

    public BlockStatementBlueprint? WhenTrue { get; set; }

    public StatementBlueprint? WhenFalse { get; set; }

}