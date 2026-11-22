namespace IR.Types;

public class PrimitiveTypeBlueprint : TypeBlueprint
{
    public PrimitiveTypeBlueprint(string originalName)
        : base(originalName)
    {
    }

    public PrimitiveTypeKind Kind { get; set; }

    public string OriginalName => Name;

}