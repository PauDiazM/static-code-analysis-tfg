using IR.Expressions;
using IR.References;

namespace IR.Variables;

public abstract class VariableBlueprint : NamedBlueprint
{
    private readonly List<string> _modifiers = [];

    protected VariableBlueprint(string name, TypeReference type)
        : base(name)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public TypeReference Type { get; }

    public ExpressionBlueprint? InitialValue { get; set; }

    public bool? IsMutable { get; set; }

    public IReadOnlyList<string> Modifiers => _modifiers;

    public void AddModifier(string modifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modifier);
        _modifiers.Add(modifier);
    }
}
