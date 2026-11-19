using IR.Statements;

namespace IR.Members;

public class PropertyAccessorBlueprint : Blueprint
{
    public PropertyAccessorBlueprint(PropertyAccessorKind accessorKind)
    {
        AccessorKind = accessorKind;
    }

    public PropertyAccessorKind AccessorKind { get; }

    public BlockStatementBlueprint? Body { get; set; }

}
