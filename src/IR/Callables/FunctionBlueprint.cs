using IR.References;

namespace IR.Callables;

public class FunctionBlueprint : CallableBlueprint
{
    public FunctionBlueprint(string name, TypeReference returnType)
        : base(name, returnType)
    {
    }
}
