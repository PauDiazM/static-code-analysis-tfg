using IR.Variables;

namespace IR.Statements;

public class VariableDeclarationStatementBlueprint : StatementBlueprint
{
    private readonly List<LocalVariableBlueprint> _variables = [];

    public IReadOnlyList<LocalVariableBlueprint> Variables => _variables.AsReadOnly();

    public void AddVariable(LocalVariableBlueprint variable)
    {
        ArgumentNullException.ThrowIfNull(variable);

        if (_variables.Any(existing => existing.Id == variable.Id))
        {
            throw new ArgumentException("The same variable cannot be added more than once.", nameof(variable));
        }

        _variables.Add(variable);
    }
}
