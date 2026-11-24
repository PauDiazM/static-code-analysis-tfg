using IR.References;

namespace IR.Variables;

public class ParameterBlueprint : VariableBlueprint
{
    private readonly List<string> _passingModifiers = [];

    public ParameterBlueprint(string name, TypeReference type)
        : base(name, type)
    {
    }

    public IReadOnlyList<string> PassingModifiers => _passingModifiers;

    public void AddPassingModifier(string modifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifier);
        _passingModifiers.Add(modifier);
    }
}
