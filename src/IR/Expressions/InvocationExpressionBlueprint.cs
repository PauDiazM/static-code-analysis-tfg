using IR.Callables;

namespace IR.Expressions;

public class InvocationExpressionBlueprint : ExpressionBlueprint
{
    private readonly List<ExpressionBlueprint> _arguments = [];

    public ExpressionBlueprint? Target { get; set; }

    public IReadOnlyList<ExpressionBlueprint> Arguments => _arguments;

    public CallableBlueprint? Callable { get; set; }

    public void AddArgument(ExpressionBlueprint argument)
    {
        ArgumentNullException.ThrowIfNull(argument);

        if (_arguments.Any(existing => ReferenceEquals(existing, argument)))
            throw new ArgumentException(
                "The same expression cannot be added more than once.",
                nameof(argument));

        _arguments.Add(argument);
    }
}
