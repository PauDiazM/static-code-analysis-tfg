using IR.Variables;
using IR.Members;
using IR.Callables;

namespace IR.Expressions;

public class MemberAccessExpressionBlueprint : ExpressionBlueprint
{
    public MemberAccessExpressionBlueprint(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public ExpressionBlueprint? ParentExpression { get; set; }

    public string Name { get; }

    public Blueprint? Declaration { get; set; }

    public void SetDeclaration(Blueprint declaration)
    {
        Declaration = declaration ?? throw new ArgumentNullException(nameof(declaration));

        if (declaration is VariableBlueprint variable)
        {
            SetResultType(variable.Type);
        }
        else if (declaration is PropertyBlueprint property)
        {
            SetResultType(property.Type);
        }
        else if (declaration is CallableBlueprint callable)
        {
            SetResultType(callable.ReturnType);
        }
    }
}