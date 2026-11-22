using IR.References;

namespace IR.Variables;

public class LocalVariableBlueprint : VariableBlueprint
{
    public LocalVariableBlueprint(string name, TypeReference type)
        : base(name, type)
    {
    }
}
