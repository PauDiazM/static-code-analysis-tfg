namespace IR.Expressions;

public class IndexerExpressionBlueprint : ExpressionBlueprint
{
    private readonly List<ExpressionBlueprint> _indices = [];

    public ExpressionBlueprint? Target { get; set; }

    public IReadOnlyList<ExpressionBlueprint> Indices => _indices;

    public void AddIndex(ExpressionBlueprint index)
    {
        ArgumentNullException.ThrowIfNull(index);

        if (_indices.Any(existing => existing.Id == index.Id))
        {
            throw new ArgumentException("The same index cannot be added more than once.", nameof(index));
        }

        _indices.Add(index);
    }
}
