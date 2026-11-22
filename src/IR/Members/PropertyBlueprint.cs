using IR.References;

namespace IR.Members;

public class PropertyBlueprint : NamedBlueprint, IMember
{
    public PropertyBlueprint(
        string name,
        Guid containerId,
        TypeReference type)
        : base(name)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        ContainerId = containerId != Guid.Empty
            ? containerId
            : throw new ArgumentException("Container ID cannot be empty.", nameof(containerId));
    }

    public TypeReference Type { get; }

    public Guid ContainerId { get; }

    public PropertyAccessorBlueprint? Getter { get; private set; }

    public PropertyAccessorBlueprint? Setter { get; private set; }

    public void SetGetter(PropertyAccessorBlueprint getter)
    {
        ArgumentNullException.ThrowIfNull(getter);
        if (getter.AccessorKind != PropertyAccessorKind.Get)
        {
            throw new ArgumentException("The getter accessor must have kind Get.", nameof(getter));
        }

        Getter = getter;
    }

    public void SetSetter(PropertyAccessorBlueprint setter)
    {
        ArgumentNullException.ThrowIfNull(setter);
        if (setter.AccessorKind != PropertyAccessorKind.Set)
        {
            throw new ArgumentException("The setter accessor must have kind Set.", nameof(setter));
        }

        Setter = setter;
    }
}
