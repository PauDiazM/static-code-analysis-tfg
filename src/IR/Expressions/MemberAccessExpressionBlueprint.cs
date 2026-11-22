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

        ResultType = declaration switch
        {
            VariableBlueprint variable => variable.Type,
            PropertyBlueprint property => property.Type,
            CallableBlueprint callable => callable.ReturnType,
            _ => throw new ArgumentException(
                $"Unsupported declaration type: {declaration.GetType().Name}",
                nameof(declaration))
        };
    }
}