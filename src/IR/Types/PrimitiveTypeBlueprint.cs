namespace IR.Types;

public class PrimitiveTypeBlueprint : TypeBlueprint
{
    public PrimitiveTypeBlueprint(string originalName)
        : base(originalName)
    {
    }

    public PrimitiveTypeKind Kind { get; private set; }

    public string OriginalName => Name;

    public void SetKind(PrimitiveTypeKind kind) => Kind = kind;
}