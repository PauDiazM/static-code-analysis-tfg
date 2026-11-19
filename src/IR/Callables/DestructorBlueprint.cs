namespace IR.Callables;

public class DestructorBlueprint : CallableBlueprint, IMember
{
    public DestructorBlueprint(string name, Guid containerId)
        : base(name)
    {
        ContainerId = containerId != Guid.Empty
            ? containerId
            : throw new ArgumentException("Container ID cannot be empty.", nameof(containerId));
    }

    public Guid ContainerId { get; }
}
