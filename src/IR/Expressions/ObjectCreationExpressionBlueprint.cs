using IR.Callables;
using IR.Types;

namespace IR.Expressions;

public class ObjectCreationExpressionBlueprint : ExpressionBlueprint
{
    private readonly List<ExpressionBlueprint> _arguments = [];

    public TypeBlueprint? InstantiatedType { get; private set; }

    public IReadOnlyList<ExpressionBlueprint> Arguments => _arguments;

    public CallableBlueprint? Constructor { get; set; }

    public void SetInstantiatedType(TypeBlueprint instantiatedType)
    {
        InstantiatedType = instantiatedType ?? throw new ArgumentNullException(nameof(instantiatedType));
        SetResultType(instantiatedType);
    }

    public void AddArgument(ExpressionBlueprint argument)
    {
        ArgumentNullException.ThrowIfNull(argument);

        if (_arguments.Any(existing => ReferenceEquals(existing, argument)))
        {
            throw new ArgumentException("The same argument cannot be added more than once.", nameof(argument));
        }

        _arguments.Add(argument);
    }

}
