namespace IR.Variables;

public class ParameterBlueprint : VariableBlueprint
{
    private readonly List<string> _passingModifiers = [];

    public ParameterBlueprint(string name)
        : base(name)
    {
    }

    public IReadOnlyList<string> PassingModifiers => _passingModifiers;

    public void AddPassingModifier(string modifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifier);
        _passingModifiers.Add(modifier);
    }
}
