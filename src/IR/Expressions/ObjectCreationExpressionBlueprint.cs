using IR.Callables;
using IR.References;

namespace IR.Expressions;

public class ObjectCreationExpressionBlueprint : ExpressionBlueprint
{
    private readonly List<ExpressionBlueprint> _arguments = [];

    public ObjectCreationExpressionBlueprint(TypeReference instantiatedType)
    {
        InstantiatedType = instantiatedType
            ?? throw new ArgumentNullException(nameof(instantiatedType));

        ResultType = instantiatedType;
    }

    public TypeReference InstantiatedType { get; }

    public IReadOnlyList<ExpressionBlueprint> Arguments => _arguments;

    public CallableBlueprint? Constructor { get; set; }

    public void AddArgument(ExpressionBlueprint argument)
    {
        ArgumentNullException.ThrowIfNull(argument);

        if (_arguments.Any(existing => ReferenceEquals(existing, argument)))
        {
            throw new ArgumentException(
                "The same argument cannot be added more than once.",
                nameof(argument));
        }

        _arguments.Add(argument);
    }
}