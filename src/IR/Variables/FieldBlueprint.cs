namespace IR.Variables;

public class FieldBlueprint : VariableBlueprint, IMember
{
    public FieldBlueprint(string name, Guid containerId)
        : base(name)
    {
        ContainerId = containerId != Guid.Empty
            ? containerId
            : throw new ArgumentException("Container ID cannot be empty.", nameof(containerId));
    }

    public Guid ContainerId { get; }
}
