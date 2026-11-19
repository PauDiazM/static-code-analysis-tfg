namespace IR.Organization;

public class ImportBlueprint : Blueprint
{
    public ImportBlueprint(string importedName)
    {
        ImportedName = importedName ?? throw new ArgumentNullException(nameof(importedName));
    }

    public string ImportedName { get; }

    public Blueprint? Declaration { get; set; }

    public string? Alias { get; set; }

    public bool IsGlobal { get; set; }

    public bool IsStatic { get; set; }

}