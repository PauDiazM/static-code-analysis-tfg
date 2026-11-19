using IR.Statements;
using IR.Types;
using IR.Variables;

namespace IR.Callables;

public abstract class CallableBlueprint : NamedBlueprint
{
    private readonly List<ParameterBlueprint> _parameters = [];

    protected CallableBlueprint(string name)
        : base(name)
    {
    }

    public TypeBlueprint? ReturnType { get; private set; }

    public IReadOnlyList<ParameterBlueprint> Parameters => _parameters;

    public BlockStatementBlueprint? Body { get; private set; }

    public CallableModifiers Modifiers { get; private set; }

    public void AddParameter(ParameterBlueprint parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        if (_parameters.Any(p => p.Id == parameter.Id))
            throw new InvalidOperationException(
                $"A parameter with ID '{parameter.Id}' already exists.");

        _parameters.Add(parameter);
    }

    public void SetBody(BlockStatementBlueprint? body) => Body = body;

    public void SetModifiers(CallableModifiers modifiers) => Modifiers = modifiers;

    protected void SetReturnType(TypeBlueprint? returnType) => ReturnType = returnType;
}