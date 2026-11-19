using IR.Expressions;
using IR.Types;

namespace IR.Variables;

public abstract class VariableBlueprint : NamedBlueprint
{
    private readonly List<string> _modifiers = [];

    protected VariableBlueprint(string name)
        : base(name)
    {
    }

    public TypeBlueprint? Type { get; set; }

    public ExpressionBlueprint? InitialValue { get; set; }

    public bool? IsMutable { get; set; }

    public IReadOnlyList<string> Modifiers => _modifiers;

    public void AddModifier(string modifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifier);
        _modifiers.Add(modifier);
    }
}
