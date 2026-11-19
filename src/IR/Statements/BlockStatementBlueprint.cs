namespace IR.Statements;

public class BlockStatementBlueprint : StatementBlueprint
{
    private readonly List<StatementBlueprint> _statements = [];

    public IReadOnlyList<StatementBlueprint> Statements => _statements.AsReadOnly();

    public void AddStatement(StatementBlueprint statement)
    {
        ArgumentNullException.ThrowIfNull(statement);

        if (_statements.Any(existing => ReferenceEquals(existing, statement)))
        {
            throw new ArgumentException("The same statement cannot be added more than once.", nameof(statement));
        }

        _statements.Add(statement);
    }
}
