using IR.Organization;
using Parsers;

namespace Translation.Mappers;

public class ImportMapper
{
    public ImportBlueprint Map(ParsedElement element)
    {
        if (element is not ImportElement import)
        {
            throw new ArgumentException(
                "Expected an ImportElement.",
                nameof(element));
        }

        return new ImportBlueprint(import.ImportedName)
        {
            Alias = import.Alias
        };
    }
}
