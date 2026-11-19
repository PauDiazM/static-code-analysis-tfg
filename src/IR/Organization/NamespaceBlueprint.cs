namespace IR.Organization;

public class NamespaceBlueprint : NamedBlueprint, IContainerBlueprint
{
    private readonly List<Blueprint> _declarations = [];

    public NamespaceBlueprint(string name)
        : base(name)
    {
    }

    public IReadOnlyList<Blueprint> Declarations => _declarations.AsReadOnly();

    public void AddDeclaration(Blueprint declaration)
    {
        ArgumentNullException.ThrowIfNull(declaration);

        if (_declarations.Any(existing => existing.Id == declaration.Id))
        {
            throw new ArgumentException("The same declaration cannot be added more than once.", nameof(declaration));
        }

        _declarations.Add(declaration);
    }
}