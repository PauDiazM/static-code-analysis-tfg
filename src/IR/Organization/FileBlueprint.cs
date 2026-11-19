namespace IR.Organization;

public class FileBlueprint : Blueprint, IContainerBlueprint
{
    private readonly List<ImportBlueprint> _imports = [];
    private readonly List<Blueprint> _declarations = [];

    public FileBlueprint(
        string filePath,
        string languageId)
    {
        FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        LanguageId = languageId ?? throw new ArgumentNullException(nameof(languageId));
    }

    public string FilePath { get; }

    public string LanguageId { get; }

    public IReadOnlyList<ImportBlueprint> Imports => _imports.AsReadOnly();

    public IReadOnlyList<Blueprint> Declarations => _declarations.AsReadOnly();

    public IEnumerable<NamespaceBlueprint> Namespaces => Declarations.OfType<NamespaceBlueprint>();

    public void AddImport(ImportBlueprint import) => AddUnique(_imports, import, nameof(import));

    public void AddDeclaration(Blueprint declaration) =>
    AddUnique(_declarations, declaration, nameof(declaration));

    private static void AddUnique<T>(List<T> items, T item, string parameterName)
        where T : Blueprint
    {
        ArgumentNullException.ThrowIfNull(item, parameterName);

        if (items.Any(existing => existing.Id == item.Id))
        {
            throw new ArgumentException(
                "An element with the same ID already exists.",
                parameterName);
        }

        items.Add(item);
    }
}