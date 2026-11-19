using IR.Expressions;
using IR.Types;
using IR.Variables;

namespace IR.Statements;

public class LoopStatementBlueprint : StatementBlueprint
{
    private readonly List<StatementBlueprint> _initializations = [];
    private readonly List<ExpressionBlueprint> _updates = [];

    public LoopKind Kind { get; set; }

    public IReadOnlyList<StatementBlueprint> Initializations => _initializations.AsReadOnly();

    public ExpressionBlueprint? Condition { get; set; }

    public IReadOnlyList<ExpressionBlueprint> Updates => _updates.AsReadOnly();

    public LocalVariableBlueprint? IterationVariable { get; set; }

    public ExpressionBlueprint? IterationSource { get; set; }

    public BlockStatementBlueprint? Body { get; set; }

    public void AddInitialization(StatementBlueprint initialization) =>
        AddUnique(_initializations, initialization, nameof(initialization));

    public void AddUpdate(ExpressionBlueprint update) => AddUnique(_updates, update, nameof(update));

    private static void AddUnique<T>(List<T> items, T item, string parameterName)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(item, parameterName);

        if (items.Any(existing => ReferenceEquals(existing, item)))
        {
            throw new ArgumentException("The same element cannot be added more than once.", parameterName);
        }

        items.Add(item);
    }
}