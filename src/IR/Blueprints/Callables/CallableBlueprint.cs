using IR.Statements;
using IR.References;
using IR.Variables;

namespace IR.Callables;

public abstract class CallableBlueprint : NamedBlueprint
{
    private readonly List<ParameterBlueprint> _parameters = [];

     protected CallableBlueprint(string name, TypeReference returnType)
        : base(name)
    {
        ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
    }

    public TypeReference ReturnType { get; }

    public IReadOnlyList<ParameterBlueprint> Parameters => _parameters;

    public BlockStatementBlueprint? Body { get; set; }

    public CallableModifiers Modifiers { get; set; }

    public void AddParameter(ParameterBlueprint parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        if (_parameters.Any(p => p.Id == parameter.Id))
            throw new InvalidOperationException(
                $"A parameter with ID '{parameter.Id}' already exists.");

        _parameters.Add(parameter);
    }

}