namespace IR;

public abstract class NamedBlueprint : Blueprint
{
    protected NamedBlueprint(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }
}
