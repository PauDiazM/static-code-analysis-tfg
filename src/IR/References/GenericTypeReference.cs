namespace IR.References;

public class GenericTypeReference : TypeReference
{
    public GenericTypeReference(TypeReference genericType)
    {
        GenericType = genericType ?? throw new ArgumentNullException(nameof(genericType));
    }

    public TypeReference GenericType { get; }

    public List<TypeReference> TypeArguments { get; } = [];
}
