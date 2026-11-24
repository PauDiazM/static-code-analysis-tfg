namespace IR.Expressions;

public enum BinaryOperatorKind
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Remainder,
    Equal,
    NotEqual,
    LessThan,
    LessThanOrEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LogicalAnd,
    LogicalOr,
    BitwiseAnd,
    BitwiseOr,
    ExclusiveOr,
    LeftShift,
    RightShift,
    UnsignedRightShift,
    NullCoalescing
}
